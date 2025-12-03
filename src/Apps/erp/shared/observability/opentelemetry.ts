import { logs, metrics } from '@opentelemetry/api';
import { SeverityNumber } from '@opentelemetry/api-logs';
import { Resource } from '@opentelemetry/resources';
import { SemanticResourceAttributes } from '@opentelemetry/semantic-conventions';
import { ZoneContextManager } from '@opentelemetry/context-zone';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { DocumentLoadInstrumentation } from '@opentelemetry/instrumentation-document-load';
import { FetchInstrumentation } from '@opentelemetry/instrumentation-fetch';
import { XMLHttpRequestInstrumentation } from '@opentelemetry/instrumentation-xml-http-request';
import { BatchSpanProcessor, WebTracerProvider } from '@opentelemetry/sdk-trace-web';
import { OTLPTraceExporter } from '@opentelemetry/exporter-trace-otlp-http';
import {
  BatchLogRecordProcessor,
  LoggerProvider,
} from '@opentelemetry/sdk-logs';
import {
  MeterProvider,
  PeriodicExportingMetricReader,
} from '@opentelemetry/sdk-metrics';
import { OTLPLogExporter } from '@opentelemetry/exporter-logs-otlp-http';
import { OTLPMetricExporter } from '@opentelemetry/exporter-metrics-otlp-http';

const baseCollectorUrl = (import.meta.env.VITE_OTEL_COLLECTOR_URL || 'http://localhost:4318').replace(/\/$/, '');
const serviceName = import.meta.env.VITE_OTEL_SERVICE_NAME || 'erp-frontend';

const resource = new Resource({
  [SemanticResourceAttributes.SERVICE_NAME]: serviceName,
  [SemanticResourceAttributes.SERVICE_NAMESPACE]: 'cardapius',
  [SemanticResourceAttributes.DEPLOYMENT_ENVIRONMENT]: import.meta.env.VITE_APP_ENV || 'dev',
});

const tracerProvider = new WebTracerProvider({ resource });
tracerProvider.addSpanProcessor(
  new BatchSpanProcessor(
    new OTLPTraceExporter({ url: `${baseCollectorUrl}/v1/traces` }),
  ),
);
tracerProvider.register({ contextManager: new ZoneContextManager() });

const loggerProvider = new LoggerProvider({ resource });
loggerProvider.addLogRecordProcessor(
  new BatchLogRecordProcessor(
    new OTLPLogExporter({ url: `${baseCollectorUrl}/v1/logs` }),
  ),
);
logs.setGlobalLoggerProvider(loggerProvider);

const meterProvider = new MeterProvider({ resource });
meterProvider.addMetricReader(
  new PeriodicExportingMetricReader({
    exporter: new OTLPMetricExporter({ url: `${baseCollectorUrl}/v1/metrics` }),
    exportIntervalMillis: 30_000,
  }),
);
metrics.setGlobalMeterProvider(meterProvider);

const meter = meterProvider.getMeter(serviceName);
meter.createObservableGauge('app.up', {
  description: 'Indica que a aplicação React está ativa para telemetria',
}).addCallback((observableResult) => {
  observableResult.observe(1);
});

const telemetryLogger = loggerProvider.getLogger(serviceName);
telemetryLogger.emit({
  severityNumber: SeverityNumber.INFO,
  body: 'Frontend telemetry initialized',
  attributes: {
    'cardapius.app': serviceName,
    'cardapius.environment': import.meta.env.VITE_APP_ENV || 'dev',
  },
});

registerInstrumentations({
  instrumentations: [
    new DocumentLoadInstrumentation(),
    new FetchInstrumentation(),
    new XMLHttpRequestInstrumentation(),
  ],
});
