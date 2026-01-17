from __future__ import annotations

import logging
from typing import List

try:
    from ultralytics import YOLO
except ImportError:  # pragma: no cover - optional dependency
    YOLO = None

from models.interfaces import BoundingBox, DetectionResult


class PersonDetector:
    def __init__(self, model_name: str = "yolov8n.pt") -> None:
        self.logger = logging.getLogger(__name__)
        self.model = None
        if YOLO is not None:
            try:
                self.model = YOLO(model_name)
            except Exception as exc:  # pragma: no cover - external dependency
                self.logger.warning("Failed to load YOLO model", extra={"error": str(exc)})
                self.model = None

    def detect(self, frame) -> DetectionResult:
        if self.model is None:
            return DetectionResult(
                event_type="person_count",
                confidence=0.0,
                severity=1,
                count=0,
                bboxes=[],
                notes="stub_person_detector",
            )

        results = self.model(frame, verbose=False)
        bboxes: List[BoundingBox] = []
        for result in results:
            for box in result.boxes:
                cls = int(box.cls.item())
                if cls != 0:
                    continue
                x1, y1, x2, y2 = [int(v) for v in box.xyxy[0].tolist()]
                conf = float(box.conf.item())
                bboxes.append(BoundingBox("person", x1, y1, x2, y2, conf))
        count = len(bboxes)
        confidence = max((bbox.conf for bbox in bboxes), default=0.0)
        return DetectionResult(
            event_type="person_count",
            confidence=confidence,
            severity=1,
            count=count,
            bboxes=bboxes,
            notes="yolo_person_detector",
        )
