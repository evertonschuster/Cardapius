import pytest

from emitters import rabbitmq_emitter
from emitters.rabbitmq_emitter import RabbitMQEmitter


def test_rabbitmq_emitter_requires_pika() -> None:
    if rabbitmq_emitter.pika is not None:
        pytest.skip("pika installed; skip runtime check")

    emitter = RabbitMQEmitter("amqp://guest:guest@localhost:5672/", "security.events")
    with pytest.raises(RuntimeError):
        emitter.publish("events.test", "{}")
