import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatTabsModule} from "@angular/material/tabs";
import {MatRadioModule} from "@angular/material/radio";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatTableModule} from "@angular/material/table";
import {MatSelectModule} from "@angular/material/select";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatCardModule} from "@angular/material/card";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatMenuModule} from "@angular/material/menu";
import {MatSidenav} from "@angular/material/sidenav";

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatTabsModule,
    MatRadioModule,
    MatPaginatorModule,
    MatTableModule,
    MatSelectModule,
    MatFormFieldModule,
    MatCardModule,
    MatDatepickerModule,
    MatMenuModule,
    MatSidenav
  ],
  exports: [
    MatTabsModule,
    MatRadioModule,
    MatPaginatorModule,
    MatTableModule,
    MatSelectModule,
    MatFormFieldModule,
    MatCardModule,
    MatDatepickerModule,
    MatMenuModule,
    MatSidenav
  ]
})
export class MaterialModule { }