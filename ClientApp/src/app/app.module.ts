import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AppComponent} from "./app.component";
import {SweetAlert2Module} from "@sweetalert2/ngx-sweetalert2";
import {RouterLink, RouterModule} from "@angular/router";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {JwtModule} from "@auth0/angular-jwt";
import {routes} from "./app.routes";
import {LoginComponent} from "@modules/login/login.component";
import {RegisterComponent} from "@modules/register/register.component";
import {ProductManagementComponent} from "@products/product-management/product-management.component";
import {ProductsListComponent} from "@products/products-list/products-list.component";
import {PublicModule} from "@public/public.module";
import {LayoutComponent} from "./layout/layout.component";
import {authInterceptor} from "@public/interceptors/auth-interceptor.interceptor";
import {ErrorHandlerService} from "@core/services/error-handler-service.service";
import {UserManagementComponent} from "./users/user-management/user-management.component";
import {UsersListComponent} from "./users/users-list/users-list.component";


export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ProductManagementComponent,
    ProductsListComponent,
    LayoutComponent,
    UserManagementComponent,
    UsersListComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    PublicModule,
    RouterModule.forRoot(routes),
    SweetAlert2Module,
    BrowserModule,
    BrowserAnimationsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['webapi.fullstackdevtest.orb.local'],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    provideHttpClient(withInterceptors(
      [authInterceptor, ErrorHandlerService]
    ))
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule {
}
