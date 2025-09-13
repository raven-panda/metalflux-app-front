import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: "", loadChildren: () => import('./public/public-module').then(m => m.PublicModule) },
  { path: "auth", loadChildren: () => import('./auth/auth-module').then(m => m.AuthModule) },
];
