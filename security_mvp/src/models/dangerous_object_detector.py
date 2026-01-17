from __future__ import annotations

from models.interfaces import DetectionResult


class DangerousObjectDetector:
    def __init__(self) -> None:
        self.model_version = "stub"

    def detect(self, frame) -> DetectionResult:
        return DetectionResult(
            event_type="dangerous_object",
            confidence=0.0,
            severity=1,
            count=0,
            bboxes=[],
            notes="placeholder_only_not_for_production",
        )
