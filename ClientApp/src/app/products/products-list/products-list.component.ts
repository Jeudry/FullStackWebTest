import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {HttpClient} from "@angular/common/http";
import {
  BehaviorSubject,
  catchError,
  debounceTime,
  map,
  merge,
  Observable,
  of,
  ReplaySubject,
  share,
  startWith,
  switchMap
} from "rxjs";
import {ProductsResponse} from "@core/interfaces/ProductsResponse";
import {ProductsService} from "@products/products.service";
import {ListResponse} from "@core/interfaces/ListResponse";
import Swal from "sweetalert2";

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrl: './products-list.component.sass'
})
export class ProductsListComponent implements AfterViewInit {
  displayedColumns: string[] = ['name', 'description', 'price', 'stock', 'createdAt', 'updatedAt'];
  data$: Observable<ProductsResponse[]> = of([]);
  dataSet: ReplaySubject<ProductsResponse[]> = new ReplaySubject<ProductsResponse[]>();

  resultsLength = 0;
  isLoadingResults = true;
  search: BehaviorSubject<string> = new BehaviorSubject<string>('');

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _httpClient: HttpClient,
    private productsService: ProductsService
  ) {
  }

  applyFilter(target: any) {
    const filterValue = target.value;
    this.search.next(filterValue);
  }

  syncData(search?: string) {
    this.isLoadingResults = true;
    return this.productsService.getProducts
    (this.sort.active, this.sort.direction, this.paginator.pageSize, this.paginator.pageIndex, search).pipe(catchError((err) => {
      this.isLoadingResults = false;
      console.error(err);
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Something went wrong!',
      }).then(() => {

      });
      return of(null);
    }));
  }

  mapData(data: ListResponse<ProductsResponse> | null) {
    this.isLoadingResults = false;

    if (data === null) {
      return [];
    }

    this.resultsLength = data.total;
    return data.items;
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

    this.data$ = this.dataSet.pipe(
      switchMap((data) => {
        return of(data);
      }),
      share()
    )

    merge(this.search).pipe(
      debounceTime(300),
      startWith(''),
      switchMap((search: string) => {
        return this.syncData(search);
      }),
      map((data: ListResponse<ProductsResponse> | null) => {
        return this.mapData(data);
      }),
      share()
    ).subscribe({
      next: (data) => {
        this.dataSet.next(data);
      }
    });

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(search => {
          return this.syncData();
        }),
        map((data: ListResponse<ProductsResponse> | null) => {
          return this.mapData(data);
        }),
        share()
      )
      .subscribe({
        next: (data) => {
          this.dataSet.next(data);
        }
      });
  }
}
