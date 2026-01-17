from __future__ import annotations

from typing import List

from models.interfaces import DetectionResult, BoundingBox


class FallDetector:
    def __init__(self, aspect_ratio_threshold: float = 1.2) -> None:
        self.aspect_ratio_threshold = aspect_ratio_threshold

    def detect(self, bboxes: List[BoundingBox]) -> DetectionResult:
        fall_candidates = []
        for bbox in bboxes:
            width = max(bbox.x2 - bbox.x1, 1)
            height = max(bbox.y2 - bbox.y1, 1)
            ratio = width / height
            if ratio > self.aspect_ratio_threshold:
                fall_candidates.append(bbox)
        confidence = 0.6 if fall_candidates else 0.0
        severity = 3 if fall_candidates else 1
        return DetectionResult(
            event_type="fall",
            confidence=confidence,
            severity=severity,
            count=len(fall_candidates),
            bboxes=fall_candidates,
            notes="heuristic_fall_detector",
        )
