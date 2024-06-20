import {Injectable} from '@angular/core';
import {config} from "@env/config.dev";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {ListResponse} from "@core/interfaces/ListResponse";
import {ProductsResponse} from "@core/interfaces/ProductsResponse";
import {CustomEncoder} from "@core/helpers/custom-encoder";

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  apiUrl = config.apiURL + "products"

  constructor(
    private readonly httpClient: HttpClient
  ) {

  }

  getProducts(sortBy: string, sortDirection: string,
              limit: number, offset: number, search?: string
  ): Observable<ListResponse<ProductsResponse>> {
    let params = new HttpParams({encoder: new CustomEncoder()})
    params = params.append('sortBy', sortBy);
    params = params.append('direction', sortDirection);
    params = params.append('limit', limit);
    params = params.append('offset', offset);
    if (search) {
      params = params.append('search', search);
    }
    return this.httpClient.get(this.apiUrl, {params}) as Observable<ListResponse<ProductsResponse>>;
  }
}
