from __future__ import annotations

from dataclasses import dataclass, field
from pathlib import Path
from typing import Any, Dict, List, Optional
import os
import yaml


@dataclass
class CameraConfig:
    camera_id: str
    url: str
    fps: int = 5


@dataclass
class QueueConfig:
    url: str = "amqp://guest:guest@rabbitmq:5672/"
    exchange: str = "security.events"


@dataclass
class AppConfig:
    cameras: List[CameraConfig]
    queue: QueueConfig = field(default_factory=QueueConfig)
    sample_fps: int = 5
    resize_width: int = 640
    resize_height: int = 360
    window_secs: int = 5
    rate_limit_secs: int = 10
    save_blurred_snapshots: bool = False
    snapshot_dir: str = "snapshots"
    http_host: str = "0.0.0.0"
    http_port: int = 8080
    log_level: str = "INFO"

    @classmethod
    def load(cls, path: Optional[str] = None) -> "AppConfig":
        data: Dict[str, Any] = {}
        if path:
            yaml_path = Path(path)
            if yaml_path.exists():
                with yaml_path.open("r", encoding="utf-8") as handle:
                    data = yaml.safe_load(handle) or {}

        env_camera_urls = os.getenv("CAMERA_URLS")
        if env_camera_urls:
            cameras = []
            for idx, url in enumerate(env_camera_urls.split(",")):
                cameras.append({"camera_id": f"cam{idx+1}", "url": url.strip()})
            data["cameras"] = cameras

        if "queue" in data:
            queue_data = data["queue"] or {}
        else:
            queue_data = {}

        if os.getenv("QUEUE_URL"):
            queue_data["url"] = os.getenv("QUEUE_URL")
        if os.getenv("QUEUE_EXCHANGE"):
            queue_data["exchange"] = os.getenv("QUEUE_EXCHANGE")
        data["queue"] = queue_data

        http_host = os.getenv("HTTP_HOST")
        if http_host:
            data["http_host"] = http_host
        http_port = os.getenv("HTTP_PORT")
        if http_port:
            data["http_port"] = int(http_port)
        log_level = os.getenv("LOG_LEVEL")
        if log_level:
            data["log_level"] = log_level

        cameras_data = data.get("cameras")
        if not cameras_data:
            raise ValueError("At least one camera must be configured")

        cameras = [CameraConfig(**camera) for camera in cameras_data]
        queue = QueueConfig(**data.get("queue", {}))

        return cls(
            cameras=cameras,
            queue=queue,
            sample_fps=int(data.get("sample_fps", 5)),
            resize_width=int(data.get("resize_width", 640)),
            resize_height=int(data.get("resize_height", 360)),
            window_secs=int(data.get("window_secs", 5)),
            rate_limit_secs=int(data.get("rate_limit_secs", 10)),
            save_blurred_snapshots=bool(data.get("save_blurred_snapshots", False)),
            snapshot_dir=str(data.get("snapshot_dir", "snapshots")),
            http_host=str(data.get("http_host", "0.0.0.0")),
            http_port=int(data.get("http_port", 8080)),
            log_level=str(data.get("log_level", "INFO")),
        )
