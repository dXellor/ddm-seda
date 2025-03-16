import { Routes } from '@angular/router';
import { SignUpInPageComponent } from './pages/sign-up-in-page/sign-up-in-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { authGuard } from './guards/auth.guard';
import { anonGuard } from './guards/anon.guard';

export const routes: Routes = [
  {
    path: '',
    component: HomePageComponent,
    pathMatch: 'full',
    canActivate: [authGuard],
  },
  {
    path: 'signupin',
    component: SignUpInPageComponent,
    pathMatch: 'full',
    canActivate: [anonGuard],
  },
];
