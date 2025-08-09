import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import { AppRoutes } from './routes';
import { Layout } from '@shared/ui';
import { AuthProvider } from '@shared/auth';

const App = () => (
  <BrowserRouter>
    <AuthProvider>
      <Layout>
        <AppRoutes />
      </Layout>
    </AuthProvider>
  </BrowserRouter>
);

export default App;
