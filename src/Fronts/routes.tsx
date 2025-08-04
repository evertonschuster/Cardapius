import React from 'react';
import { RouteObject, useRoutes } from 'react-router-dom';
import { AdminDashboard } from '@modules/admin';
import { PdvSales } from '@modules/pdv';
import { KitchenOrders } from '@modules/smart-kitchen';
import { InventoryOverview } from '@modules/estoque';
import { PrivateRoute } from '@shared/auth';

const routes: RouteObject[] = [
  { path: '/login', element: <div>Login TODO</div> },
  {
    path: '/',
    element: <PrivateRoute />,
    children: [
      { path: 'admin', element: <AdminDashboard /> },
      { path: 'pdv', element: <PdvSales /> },
      { path: 'smart-kitchen', element: <KitchenOrders /> },
      { path: 'estoque', element: <InventoryOverview /> }
    ]
  }
];

export const AppRoutes = () => useRoutes(routes);
