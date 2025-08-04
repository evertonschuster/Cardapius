import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import { AppRoutes } from './routes';
import { Layout } from '@shared/ui';

const App = () => (
  <BrowserRouter>
    <Layout>
      <AppRoutes />
    </Layout>
  </BrowserRouter>
);

export default App;
