import {Routes} from '@angular/router';
import {LoginComponent} from "@modules/login/login.component";
import {RegisterComponent} from "@modules/register/register.component";
import {LayoutComponent} from "./layout/layout.component";
import {ProductsListComponent} from "@products/products-list/products-list.component";
import {AuthGuard} from "@core/guards/auth.guard";
import {UsersListComponent} from "./users/users-list/users-list.component";
import {AdminGuard} from "@core/guards/admin.guard";

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
      },
      {
        canActivate: [AdminGuard],
        path: 'users',
        component: UsersListComponent
      },
      {
        path: '**',
        redirectTo: 'products'
      }
    ]
  }
];
