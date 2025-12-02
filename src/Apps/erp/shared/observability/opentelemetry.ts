import { metrics } from '@opentelemetry/api';
import { logs, SeverityNumber } from '@opentelemetry/api-logs';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { DocumentLoadInstrumentation } from '@opentelemetry/instrumentation-document-load';
import { FetchInstrumentation } from '@opentelemetry/instrumentation-fetch';
import { XMLHttpRequestInstrumentation } from '@opentelemetry/instrumentation-xml-http-request';
import { OTLPLogExporter } from '@opentelemetry/exporter-logs-otlp-http';
import { OTLPMetricExporter } from '@opentelemetry/exporter-metrics-otlp-http';
import { OTLPTraceExporter } from '@opentelemetry/exporter-trace-otlp-http';
import { LoggerProvider, BatchLogRecordProcessor } from '@opentelemetry/sdk-logs';
import { MeterProvider, PeriodicExportingMetricReader } from '@opentelemetry/sdk-metrics';
import { Resource } from '@opentelemetry/resources';
import { SemanticResourceAttributes } from '@opentelemetry/semantic-conventions';
import { BatchSpanProcessor } from '@opentelemetry/sdk-trace-base';
import { WebTracerProvider } from '@opentelemetry/sdk-trace-web';

import { getEnv } from '../utils';

const SERVICE_NAME = 'erp-front';
const DEFAULT_OTLP_ENDPOINT = 'http://localhost:4318';

let initialized = false;

const getOtlpEndpoint = () => {
  const fromImportMeta = (() => {
    try {
      return (0, eval)('import.meta')?.env?.VITE_OTEL_EXPORTER_OTLP_ENDPOINT as string | undefined;
    } catch (error) {
      return undefined;
    }
  })();

  const fromProcess = typeof process !== 'undefined' ? process.env?.OTEL_EXPORTER_OTLP_ENDPOINT : undefined;

  return (fromImportMeta || fromProcess || DEFAULT_OTLP_ENDPOINT).replace(/\/$/, '');
};

const createResource = () =>
  new Resource({
    [SemanticResourceAttributes.SERVICE_NAME]: SERVICE_NAME,
    [SemanticResourceAttributes.DEPLOYMENT_ENVIRONMENT]: 'development',
  });

const configureTracing = (resource: Resource, otlpEndpoint: string) => {
  const traceExporter = new OTLPTraceExporter({ url: `${otlpEndpoint}/v1/traces` });
  const tracerProvider = new WebTracerProvider({ resource });
  tracerProvider.addSpanProcessor(new BatchSpanProcessor(traceExporter));
  tracerProvider.register();

  registerInstrumentations({
    instrumentations: [
      new DocumentLoadInstrumentation(),
      new FetchInstrumentation({ propagateTraceHeaderCorsUrls: [/.*/] }),
      new XMLHttpRequestInstrumentation({ propagateTraceHeaderCorsUrls: [/.*/] }),
    ],
  });
};

const configureMetrics = (resource: Resource, otlpEndpoint: string) => {
  const metricExporter = new OTLPMetricExporter({ url: `${otlpEndpoint}/v1/metrics` });
  const meterProvider = new MeterProvider({ resource });

  meterProvider.addMetricReader(
    new PeriodicExportingMetricReader({
      exporter: metricExporter,
      exportIntervalMillis: 15000,
    })
  );

  metrics.setGlobalMeterProvider(meterProvider);

  const meter = meterProvider.getMeter(SERVICE_NAME);
  const uptimeGauge = meter.createObservableGauge('client.uptime', {
    description: 'Milliseconds since page load.',
    unit: 'ms',
  });

  meter.addBatchObservableCallback(
    (observableResult) => {
      observableResult.observe(uptimeGauge, performance.now());
    },
    [uptimeGauge]
  );
};

const configureLogs = (resource: Resource, otlpEndpoint: string) => {
  const logExporter = new OTLPLogExporter({ url: `${otlpEndpoint}/v1/logs` });
  const loggerProvider = new LoggerProvider({ resource });

  loggerProvider.addLogRecordProcessor(new BatchLogRecordProcessor(logExporter));
  logs.setGlobalLoggerProvider(loggerProvider);

  const logger = logs.getLogger(SERVICE_NAME);
  logger.emit({
    severityNumber: SeverityNumber.INFO,
    severityText: 'INFO',
    body: 'Client-side OpenTelemetry initialized.',
    attributes: { 'app.environment': 'dev' },
  });
};

export const initializeObservability = () => {
  if (initialized) return;
  if (typeof window === 'undefined') return;
  if (getEnv() !== 'dev') return;

  const resource = createResource();
  const otlpEndpoint = getOtlpEndpoint();

  configureTracing(resource, otlpEndpoint);
  configureMetrics(resource, otlpEndpoint);
  configureLogs(resource, otlpEndpoint);

  initialized = true;
};

