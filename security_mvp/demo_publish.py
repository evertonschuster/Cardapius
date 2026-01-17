from __future__ import annotations

import json
import uuid
from datetime import datetime, timezone

from emitters.rabbitmq_emitter import RabbitMQEmitter


def main() -> None:
    emitter = RabbitMQEmitter("amqp://guest:guest@localhost:5672/", "security.events")
    payload = {
        "schema_version": "1.0",
        "event_id": str(uuid.uuid4()),
        "camera_id": "demo_cam",
        "timestamp_utc": datetime.now(timezone.utc).isoformat(),
        "event_type": "person_count",
        "severity": 1,
        "confidence": 0.5,
        "window_secs": 5,
        "counts": {"persons": 3},
        "bboxes": [],
        "privacy": {"faces_blurred": True, "frame_stored": False},
        "debug": {"model_versions": {}, "notes": "demo_publish"},
    }
    emitter.publish("events.demo_cam.person_count", json.dumps(payload))
    emitter.close()


if __name__ == "__main__":
    main()
