# MVP - Sistema de Eventos de Segurança (Salas de Aula)

Este MVP processa streams RTSP/arquivos de vídeo, detecta eventos de segurança e publica mensagens em RabbitMQ. **Não** identifica pessoas, não faz reconhecimento facial e não infere emoções/atenção.

## Principais características
- Detecção de pessoas (YOLOv8 opcional; fallback stub).
- Detecção de violência (stub com interface pronta para modelo real).
- Detecção de queda/pessoa no chão (heurística simples por aspect ratio).
- Detecção de “aglomerado súbito/correria” via optical flow (heurística).
- Detecção de objeto perigoso **apenas placeholder**.
- Eventos com agregação temporal e rate limit por câmera/evento.
- Emissão para RabbitMQ com exchange `topic`.
- API HTTP com `/health` e `/metrics`.
- Configuração por YAML e variáveis de ambiente.

## Estrutura
```
security_mvp/
  src/
    main.py
    config.py
    ingest/rtsp_reader.py
    pipeline/frame_sampler.py
    models/interfaces.py
    models/person_detector.py
    models/violence_detector.py
    models/fall_detector.py
    models/dangerous_object_detector.py
    postprocess/temporal_smoother.py
    emitters/queue_interface.py
    emitters/rabbitmq_emitter.py
    api/http_server.py
    utils/logging.py
  tests/
  docker/
  config.yml
```

## Configuração
Edite `config.yml` ou use variáveis de ambiente:
- `CAMERA_URLS=rtsp://...,...`
- `QUEUE_URL=amqp://guest:guest@rabbitmq:5672/`
- `QUEUE_EXCHANGE=security.events`
- `HTTP_HOST=0.0.0.0`
- `HTTP_PORT=8080`
- `LOG_LEVEL=INFO`

## Execução local
```bash
make install
make run
```

## Docker Compose
```bash
docker compose -f docker/docker-compose.yml up --build
```

## Mensagem JSON (schema v1)
```json
{
  "schema_version": "1.0",
  "event_id": "<uuid4>",
  "camera_id": "<string>",
  "timestamp_utc": "<ISO8601>",
  "event_type": "violence|fall|crowd_surge|person_count",
  "severity": 1,
  "confidence": 0.0,
  "window_secs": 5,
  "counts": { "persons": 2 },
  "bboxes": [
     {"label":"person","x1":0,"y1":0,"x2":0,"y2":0,"conf":0.0}
  ],
  "privacy": {
     "faces_blurred": true,
     "frame_stored": false
  },
  "debug": { "model_versions": {}, "notes": "" }
}
```

## Observações importantes
- O detector de pessoas usa **YOLOv8n** caso `ultralytics` esteja instalado; caso contrário retorna stub.
- O detector de violência e de objeto perigoso são stubs. Substitua pelos modelos reais.
- Por padrão **não** salva vídeo ou frames. Caso `save_blurred_snapshots` seja `true`, salva apenas snapshots borrados.
