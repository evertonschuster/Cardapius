from __future__ import annotations

import logging
from typing import Optional

try:
    import pika
except ImportError:  # pragma: no cover - optional dependency
    pika = None

from emitters.queue_interface import QueueEmitter


class RabbitMQEmitter(QueueEmitter):
    def __init__(self, url: str, exchange: str) -> None:
        self.url = url
        self.exchange = exchange
        self.logger = logging.getLogger(__name__)
        self._connection: Optional["pika.BlockingConnection"] = None
        self._channel: Optional["pika.channel.Channel"] = None

    def _connect(self) -> None:
        if pika is None:
            raise RuntimeError("pika is required for RabbitMQEmitter")
        if self._connection and self._connection.is_open:
            return
        params = pika.URLParameters(self.url)
        self._connection = pika.BlockingConnection(params)
        self._channel = self._connection.channel()
        self._channel.exchange_declare(exchange=self.exchange, exchange_type="topic", durable=True)

    def publish(self, routing_key: str, payload: str) -> None:
        self._connect()
        if not self._channel:
            raise RuntimeError("RabbitMQ channel not available")
        self._channel.basic_publish(exchange=self.exchange, routing_key=routing_key, body=payload)
        self.logger.info("Published event", extra={"routing_key": routing_key})

    def close(self) -> None:
        if self._connection and self._connection.is_open:
            self._connection.close()
