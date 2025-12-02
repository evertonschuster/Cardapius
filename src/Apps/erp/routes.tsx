import { RouteObject, useRoutes } from 'react-router-dom';
import { AdminDashboard } from '@modules/admin';
import { PdvSales } from '@modules/pdv';
import { KitchenOrders } from '@modules/smart-kitchen';
import { InventoryOverview } from '@modules/estoque';
import { PrivateRoute, Login } from '@shared/auth';
import { Callback } from '@shared/auth/pages/Callback';
import { Logout } from '@shared/auth/pages/Logout';
import { Home } from '@modules/home/pages/Home';
import { AppLayout } from '@shared/components/layout/AppLayout';

const routes: RouteObject[] = [
  { path: '/login', element: <Login /> },
  { path: '/callback', element: <Callback /> },
  { path: '/logout', element: <Logout /> },
  {
    path: '/',
    element: <AppLayout />,
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
      },
      {
        path: '',
        element: (
          <PrivateRoute>
            <Home />
          </PrivateRoute>
        )
      }
    ]
  }
];

export const AppRoutes = () => useRoutes(routes);
