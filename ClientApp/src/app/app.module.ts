import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import {AppComponent} from "./app.component";
import {SweetAlert2Module} from "@sweetalert2/ngx-sweetalert2";
import {RouterModule} from "@angular/router";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {provideHttpClient} from "@angular/common/http";
import {JwtModule} from "@auth0/angular-jwt";
import {routes} from "./app.routes";
import {config} from "@env/config.dev"
import {LoginComponent} from "@modules/login/login.component";
import {RegisterComponent} from "@modules/register/register.component";
import {ProductManagementComponent} from "@products/product-management/product-management.component";
import {ProductsListComponent} from "@products/products-list/products-list.component";
import {PublicModule} from "@public/public.module";

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
  ],
  imports: [
    CommonModule,
    PublicModule,
    RouterModule.forRoot(routes),
    SweetAlert2Module,
    BrowserModule,
    BrowserAnimationsModule,
    JwtModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [config.shortApiURL],
        disallowedRoutes: []
      }
    })
  ],
  providers: [
    provideHttpClient()
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
