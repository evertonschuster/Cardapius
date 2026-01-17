from __future__ import annotations

from models.interfaces import DetectionResult


class ViolenceDetector:
    def __init__(self) -> None:
        self.model_version = "stub"

    def detect(self, frame) -> DetectionResult:
        return DetectionResult(
            event_type="violence",
            confidence=0.0,
            severity=1,
            count=0,
            bboxes=[],
            notes="TODO: replace with action recognition model",
        )
