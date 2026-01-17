from __future__ import annotations

import time


class FrameSampler:
    def __init__(self, fps: int) -> None:
        self.interval = 1.0 / fps
        self._last_ts = 0.0

    def should_sample(self) -> bool:
        now = time.monotonic()
        if now - self._last_ts >= self.interval:
            self._last_ts = now
            return True
        return False
