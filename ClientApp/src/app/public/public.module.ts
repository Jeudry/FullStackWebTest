import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MaterialModule} from "./material/material.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {AvatarModule} from "ngx-avatars";
import {InputComponent} from "@public/components/input/input.component";
import {HeaderComponent} from "@public/components/header/header.component";
import {RouterLink, RouterLinkActive} from "@angular/router";


@NgModule({
  declarations: [
    InputComponent,
    HeaderComponent
  ],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    AvatarModule,
    RouterLink,
    RouterLinkActive
  ],
  exports: [
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    AvatarModule,
    InputComponent,
    HeaderComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PublicModule {
}
