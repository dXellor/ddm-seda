import { Routes } from '@angular/router';
import { SignUpInPageComponent } from './pages/sign-up-in-page/sign-up-in-page.component';

export const routes: Routes = [
  {
    path: 'signupin',
    component: SignUpInPageComponent,
    pathMatch: 'full',
  },
];
