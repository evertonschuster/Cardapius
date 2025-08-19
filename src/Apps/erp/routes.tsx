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
        element: (
          <PrivateRoute roles={['admin']}>
            <AdminDashboard />
          </PrivateRoute>
        )
      },
      {
        path: 'pdv',
        element: (
          <PrivateRoute roles={['pdv']}>
            <PdvSales />
          </PrivateRoute>
        )
      },
      {
        path: 'smart-kitchen',
        element: (
          <PrivateRoute roles={['smart-kitchen']}>
            <KitchenOrders />
          </PrivateRoute>
        )
      },
      {
        path: 'estoque',
        element: (
          <PrivateRoute roles={['estoque']}>
            <InventoryOverview />
          </PrivateRoute>
        )
      }
    ]
  }
];

export const AppRoutes = () => useRoutes(routes);
