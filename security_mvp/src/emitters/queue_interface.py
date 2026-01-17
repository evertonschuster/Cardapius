from __future__ import annotations

from typing import Protocol


class QueueEmitter(Protocol):
    def publish(self, routing_key: str, payload: str) -> None:
        ...
