import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {config} from "@env/config.dev";

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  private apiUrl = config + 'products';

  constructor(
    private http: HttpClient
  ) { }


}
