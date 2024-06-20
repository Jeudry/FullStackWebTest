import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import {AppComponent} from "./app.component";
import {SweetAlert2Module} from "@sweetalert2/ngx-sweetalert2";
import {RouterModule} from "@angular/router";
import {BrowserModule} from "@angular/platform-browser";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {HttpClient, HttpClientModule, provideHttpClient} from "@angular/common/http";
import {JwtModule} from "@auth0/angular-jwt";
import {routes} from "./app.routes";
import {config} from "@env/config.dev"

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
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
    }),
    routes
  ],
  providers: [
    provideHttpClient()
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
