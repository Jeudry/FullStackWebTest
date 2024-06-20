import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import {MaterialModule} from "./material/material.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {AvatarModule} from "ngx-avatars";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    AvatarModule
  ],
  exports: [
    MaterialModule,
    ReactiveFormsModule,
    FormsModule,
    AvatarModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PublicModule { }
