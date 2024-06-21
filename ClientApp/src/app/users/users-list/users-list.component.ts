import {AfterViewInit, Component, ViewChild} from '@angular/core';
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
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import Swal from "sweetalert2";
import {ListResponse} from "@core/interfaces/ListResponse";
import {UserResponse} from "@core/interfaces/userResponse";
import {AuthenticateService} from "@modules/services/authentication.service";
import {UserManagementComponent} from "../user-management/user-management.component";

export interface UserList {
  id: string;
  userName: string;
  email: string;
  role: string;

}

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.sass'
})
export class UsersListComponent implements AfterViewInit {
  displayedColumns: string[] = ['userName', 'email', 'role', 'actions'];
  data$: Observable<UserList[]> = of([]);
  dataSet: ReplaySubject<UserList[]> = new ReplaySubject<UserList[]>();

  resultsLength = 0;
  isLoadingResults = true;
  search: BehaviorSubject<string> = new BehaviorSubject<string>('');

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _httpClient: HttpClient,
    private authenticateService: AuthenticateService,
    private readonly router: Router,
    private readonly dialog: MatDialog
  ) {
  }

  createUser() {
    const dialogRef = this.dialog.open(UserManagementComponent, {
      data: {id: undefined},
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.search.next('');
      }
    });
  }

  updateUser(id: string) {
    const dialogRef = this.dialog.open(UserManagementComponent, {
      data: {id: id},
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.search.next('');
      }
    });
  }

  deleteUser(id: string) {
    this.authenticateService.deleteUser(id).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'User deleted',
          timer: 1500
        }).then(() => {
          this.search.next('');
        });
      },
      error: (err) => {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          html: err
        }).then(() => {

        });
      }
    });
  }

  applyFilter(target: any) {
    const filterValue = target.value;
    this.search.next(filterValue);
  }

  syncData(search?: string) {
    this.isLoadingResults = true;
    return this.authenticateService.getUsers
    (this.sort.active, this.sort.direction, this.paginator.pageSize, this.paginator.pageIndex, search).pipe(catchError((err) => {
      this.isLoadingResults = false;
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        html: err
      }).then(() => {

      });
      return of(null);
    }));
  }

  mapData(data: ListResponse<UserResponse> | null) {
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
      map((data: ListResponse<UserResponse> | null) => {
        return this.mapData(data);
      }),
      share()
    ).subscribe({
      next: (data) => {
        const newData = data.map((item) => {

          return {
            id: item.id,
            userName: item.userName,
            email: item.email,
            role: item.roles.length > 0 ? item.roles[0].name : 'No role'
          }
        });
        this.dataSet.next(newData);
      }
    });

    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(search => {
          return this.syncData();
        }),
        map((data: ListResponse<UserResponse> | null) => {
          return this.mapData(data);
        }),
        share()
      )
      .subscribe({
        next: (data) => {
          const newData = data.map((item) => {

            return {
              id: item.id,
              userName: item.userName,
              email: item.email,
              role: item.roles.length > 0 ? item.roles[0].name : 'No role'
            }
          });
          this.dataSet.next(newData);
        }
      });
  }
}
