from __future__ import annotations

import argparse
from datetime import datetime, timezone
import json
import logging
import os
import threading
import time
import uuid
from typing import Dict, List, Optional, Tuple

import cv2
import numpy as np
import uvicorn

from api.http_server import MetricsStore, create_app
from config import AppConfig
from emitters.rabbitmq_emitter import RabbitMQEmitter
from ingest.rtsp_reader import RTSPReader
from models.dangerous_object_detector import DangerousObjectDetector
from models.fall_detector import FallDetector
from models.interfaces import DetectionResult
from models.person_detector import PersonDetector
from models.violence_detector import ViolenceDetector
from postprocess.temporal_smoother import TemporalSmoother
from pipeline.frame_sampler import FrameSampler
from utils.logging import setup_logging


EventPayload = Dict[str, object]


def preprocess(frame: np.ndarray, size: Tuple[int, int]) -> np.ndarray:
    resized = cv2.resize(frame, size)
    return resized


def blur_frame(frame: np.ndarray) -> np.ndarray:
    return cv2.GaussianBlur(frame, (31, 31), 0)


def build_event(
    camera_id: str,
    event_type: str,
    severity: int,
    confidence: float,
    window_secs: int,
    count: int,
    bboxes: List[Dict[str, object]],
    notes: str,
) -> EventPayload:
    return {
        "schema_version": "1.0",
        "event_id": str(uuid.uuid4()),
        "camera_id": camera_id,
        "timestamp_utc": datetime.now(timezone.utc).isoformat(),
        "event_type": event_type,
        "severity": severity,
        "confidence": round(confidence, 3),
        "window_secs": window_secs,
        "counts": {"persons": count},
        "bboxes": bboxes,
        "privacy": {"faces_blurred": True, "frame_stored": False},
        "debug": {"model_versions": {}, "notes": notes},
    }


def compute_crowd_surge(prev_gray: Optional[np.ndarray], gray: np.ndarray) -> float:
    if prev_gray is None:
        return 0.0
    flow = cv2.calcOpticalFlowFarneback(
        prev_gray,
        gray,
        None,
        0.5,
        3,
        15,
        3,
        5,
        1.2,
        0,
    )
    magnitude, _ = cv2.cartToPolar(flow[..., 0], flow[..., 1])
    return float(np.mean(magnitude))


def process_camera(
    camera_id: str,
    reader: RTSPReader,
    config: AppConfig,
    sample_fps: int,
    emitter: RabbitMQEmitter,
    metrics: MetricsStore,
    stop_event: threading.Event,
) -> None:
    logger = logging.getLogger(__name__)
    sampler = FrameSampler(sample_fps)
    smoother = TemporalSmoother(config.window_secs)
    person_detector = PersonDetector()
    violence_detector = ViolenceDetector()
    fall_detector = FallDetector()
    dangerous_detector = DangerousObjectDetector()

    last_emit: Dict[str, float] = {}
    prev_gray: Optional[np.ndarray] = None

    while not stop_event.is_set():
        frame = reader.get_latest_frame()
        if frame is None:
            time.sleep(0.1)
            continue
        if not sampler.should_sample():
            time.sleep(0.01)
            continue

        metrics.frames_processed += 1
        processed = preprocess(frame, (config.resize_width, config.resize_height))
        gray = cv2.cvtColor(processed, cv2.COLOR_BGR2GRAY)

        person_result = person_detector.detect(processed)
        fall_result = fall_detector.detect(person_result.bboxes)
        violence_result = violence_detector.detect(processed)
        dangerous_result = dangerous_detector.detect(processed)

        crowd_score = compute_crowd_surge(prev_gray, gray)
        prev_gray = gray

        events = [person_result, fall_result, violence_result, dangerous_result]
        if crowd_score > 1.5:
            events.append(
                DetectionResult(
                    event_type="crowd_surge",
                    confidence=min(crowd_score / 5.0, 1.0),
                    severity=3,
                    count=person_result.count,
                    bboxes=person_result.bboxes,
                    notes="optical_flow_crowd_surge",
                )
            )

        for event in events:
            if event.event_type == "dangerous_object":
                continue
            smooth_conf = smoother.push(f"{camera_id}:{event.event_type}", event.confidence, event.count)
            if smooth_conf <= 0:
                continue
            now = time.monotonic()
            last_time = last_emit.get(event.event_type, 0.0)
            if now - last_time < config.rate_limit_secs:
                continue

            bboxes_payload = [
                {
                    "label": bbox.label,
                    "x1": bbox.x1,
                    "y1": bbox.y1,
                    "x2": bbox.x2,
                    "y2": bbox.y2,
                    "conf": round(bbox.conf, 3),
                }
                for bbox in event.bboxes
            ]

            payload = build_event(
                camera_id=camera_id,
                event_type=event.event_type,
                severity=event.severity,
                confidence=smooth_conf,
                window_secs=config.window_secs,
                count=event.count,
                bboxes=bboxes_payload,
                notes=event.notes,
            )

            if config.save_blurred_snapshots:
                os.makedirs(config.snapshot_dir, exist_ok=True)
                snapshot_path = os.path.join(
                    config.snapshot_dir, f"{camera_id}_{payload['event_id']}.jpg"
                )
                blurred = blur_frame(processed)
                cv2.imwrite(snapshot_path, blurred)

            routing_key = f"events.{camera_id}.{event.event_type}"
            emitter.publish(routing_key, json.dumps(payload))
            metrics.events_emitted += 1
            last_emit[event.event_type] = now
            logger.info("Event emitted", extra={"camera_id": camera_id, "event": event.event_type})

        time.sleep(0.01)


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--config", default="config.yml")
    args = parser.parse_args()

    config = AppConfig.load(args.config)
    setup_logging(config.log_level)

    metrics = MetricsStore()
    emitter = RabbitMQEmitter(config.queue.url, config.queue.exchange)

    stop_event = threading.Event()
    threads = []
    readers = []

    for camera in config.cameras:
        reader = RTSPReader(camera.url)
        reader.start()
        readers.append(reader)
        thread = threading.Thread(
            target=process_camera,
            args=(
                camera.camera_id,
                reader,
                config,
                camera.fps or config.sample_fps,
                emitter,
                metrics,
                stop_event,
            ),
            daemon=True,
        )
        thread.start()
        threads.append(thread)

    app = create_app(metrics)

    try:
        uvicorn.run(app, host=config.http_host, port=config.http_port, log_level="info")
    finally:
        stop_event.set()
        for reader in readers:
            reader.stop()
        emitter.close()


if __name__ == "__main__":
    main()
