import React from 'react';
import { createRoot } from 'react-dom/client';
import App from './App';
import { initializeObservability } from './shared/observability/opentelemetry';

const container = document.getElementById('root');

initializeObservability();

if (container) {
  const root = createRoot(container);
  root.render(<App />);
}
