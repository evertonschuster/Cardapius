from __future__ import annotations

from dataclasses import dataclass
from typing import List, Protocol


@dataclass
class BoundingBox:
    label: str
    x1: int
    y1: int
    x2: int
    y2: int
    conf: float


@dataclass
class DetectionResult:
    event_type: str
    confidence: float
    severity: int
    count: int
    bboxes: List[BoundingBox]
    notes: str = ""


class Detector(Protocol):
    def detect(self, frame) -> DetectionResult:
        ...
