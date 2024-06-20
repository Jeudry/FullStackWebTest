import {Routes} from '@angular/router';
import {LoginComponent} from "@modules/login/login.component";
import {RegisterComponent} from "@modules/register/register.component";
import {LayoutComponent} from "./layout/layout.component";
import {ProductsListComponent} from "@products/products-list/products-list.component";
import {AuthGuard} from "@core/guards/auth.guard";

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'home',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'products',
        component: ProductsListComponent
      }
    ]
  }
];
