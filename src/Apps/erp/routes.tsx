import React from 'react';
import { RouteObject, useRoutes } from 'react-router-dom';
import { AdminDashboard } from '@modules/admin';
import { PdvSales } from '@modules/pdv';
import { KitchenOrders } from '@modules/smart-kitchen';
import { InventoryOverview } from '@modules/estoque';
import { PrivateRoute, Login } from '@shared/auth';

const routes: RouteObject[] = [
  { path: '/login', element: <Login /> },
  {
    path: '/',
    element: <PrivateRoute />,
    children: [
      {
        path: 'admin',
        element: <PrivateRoute roles={['admin']} />,
        children: [{ index: true, element: <AdminDashboard /> }]
      },
      {
        path: 'pdv',
        element: <PrivateRoute roles={['pdv']} />,
        children: [{ index: true, element: <PdvSales /> }]
      },
      {
        path: 'smart-kitchen',
        element: <PrivateRoute roles={['smart-kitchen']} />,
        children: [{ index: true, element: <KitchenOrders /> }]
      },
      {
        path: 'estoque',
        element: <PrivateRoute roles={['estoque']} />,
        children: [{ index: true, element: <InventoryOverview /> }]
      }
    ]
  }
];

export const AppRoutes = () => useRoutes(routes);
