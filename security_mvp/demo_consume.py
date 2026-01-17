from __future__ import annotations

import json

import pika


def main() -> None:
    connection = pika.BlockingConnection(pika.URLParameters("amqp://guest:guest@localhost:5672/"))
    channel = connection.channel()
    channel.exchange_declare(exchange="security.events", exchange_type="topic", durable=True)
    result = channel.queue_declare(queue="", exclusive=True)
    queue_name = result.method.queue
    channel.queue_bind(exchange="security.events", queue=queue_name, routing_key="events.#")

    def callback(ch, method, properties, body):
        payload = json.loads(body)
        print("[x]", payload)

    channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)
    print("[*] Waiting for messages. To exit press CTRL+C")
    channel.start_consuming()


if __name__ == "__main__":
    main()
