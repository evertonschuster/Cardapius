from __future__ import annotations

import logging
import threading
import time
from typing import Optional

import cv2


class RTSPReader:
    def __init__(self, url: str, reconnect_secs: float = 5.0) -> None:
        self.url = url
        self.reconnect_secs = reconnect_secs
        self._capture: Optional[cv2.VideoCapture] = None
        self._lock = threading.Lock()
        self._latest_frame = None
        self._stop_event = threading.Event()
        self._thread: Optional[threading.Thread] = None
        self.logger = logging.getLogger(__name__)

    def start(self) -> None:
        if self._thread and self._thread.is_alive():
            return
        self._stop_event.clear()
        self._thread = threading.Thread(target=self._loop, daemon=True)
        self._thread.start()

    def stop(self) -> None:
        self._stop_event.set()
        if self._thread:
            self._thread.join(timeout=2)
        if self._capture:
            self._capture.release()

    def get_latest_frame(self):
        with self._lock:
            return self._latest_frame

    def _connect(self) -> Optional[cv2.VideoCapture]:
        capture = cv2.VideoCapture(self.url)
        if not capture.isOpened():
            capture.release()
            return None
        return capture

    def _loop(self) -> None:
        while not self._stop_event.is_set():
            if self._capture is None or not self._capture.isOpened():
                self.logger.warning("Connecting to camera", extra={"url": self.url})
                self._capture = self._connect()
                if not self._capture:
                    time.sleep(self.reconnect_secs)
                    continue

            ok, frame = self._capture.read()
            if not ok:
                self.logger.warning("Camera read failed", extra={"url": self.url})
                self._capture.release()
                self._capture = None
                time.sleep(self.reconnect_secs)
                continue

            with self._lock:
                self._latest_frame = frame
            time.sleep(0.001)
