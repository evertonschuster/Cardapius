from __future__ import annotations

from collections import defaultdict, deque
from dataclasses import dataclass
from typing import Deque, Dict
import time


@dataclass
class EventWindow:
    timestamp: float
    confidence: float
    count: int


class TemporalSmoother:
    def __init__(self, window_secs: int) -> None:
        self.window_secs = window_secs
        self._events: Dict[str, Deque[EventWindow]] = defaultdict(deque)

    def push(self, key: str, confidence: float, count: int) -> float:
        now = time.monotonic()
        window = self._events[key]
        window.append(EventWindow(now, confidence, count))
        while window and now - window[0].timestamp > self.window_secs:
            window.popleft()
        if not window:
            return 0.0
        return sum(item.confidence for item in window) / len(window)
