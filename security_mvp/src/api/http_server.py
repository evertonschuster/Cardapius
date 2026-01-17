from __future__ import annotations

from dataclasses import dataclass
from fastapi import FastAPI


@dataclass
class MetricsStore:
    frames_processed: int = 0
    events_emitted: int = 0


def create_app(metrics: MetricsStore) -> FastAPI:
    app = FastAPI()

    @app.get("/health")
    def health() -> dict:
        return {"status": "ok"}

    @app.get("/metrics")
    def metrics_endpoint() -> dict:
        return {
            "frames_processed": metrics.frames_processed,
            "events_emitted": metrics.events_emitted,
        }

    return app
