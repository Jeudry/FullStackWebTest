import {Injectable} from '@angular/core';
import {config} from "@env/config.dev";
import {Observable, Subject} from "rxjs";
import {HttpClient, HttpParams} from "@angular/common/http";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {UserResponse} from "@core/interfaces/userResponse";
import {CreateDto, RegisterDto} from "@core/interfaces/registerDto";
import {LogingDto} from "@core/interfaces/logingDto";
import {LoginResponse} from "@core/interfaces/loginResponse";
import {CustomEncoder} from "@core/helpers/custom-encoder";
import {ListResponse} from "@core/interfaces/ListResponse";
import {UserDto} from "@core/interfaces/userDto";
import {RoleResponse} from "@core/interfaces/RoleResponse";

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {
  private apiUrl = `${config.apiURL}users/`
  private authChangeSub = new Subject<boolean>()

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) {
  }

  getCurrentDetailUser(): Observable<UserResponse> {
    return this.http.get<UserResponse>(`${this.apiUrl}get-current-profile`);
  }

  register = (body: RegisterDto) => {
    return this.http.post(`${this.apiUrl}`, body);
  }

  login = (body: LogingDto) => {
    return this.http.post<LoginResponse>(`${this.apiUrl}login`, body);
  }

  sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("token");
    return Boolean(token && !this.jwtHelper.isTokenExpired(token))
  }

  public isUserAdmin = (): boolean => {
    const token = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token!);
    const role = decodedToken.role;
    return role === 'Admin';
  }

  public logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
    this.sendAuthStateChangeNotification(false);
    this.router.navigate(['/']).then(() => {
      window.location.reload();
    });
  }

  deleteUser(id: string) {
    return this.http.delete(`${this.apiUrl}${id}`);
  }

  updateUser(id: string, body: UserDto) {
    return this.http.put(`${this.apiUrl}${id}`, body);
  }

  createUser(body: CreateDto) {
    return this.http.post(`${this.apiUrl}create`, body);
  }

  getUser(id: string) {
    return this.http.get<UserResponse>(`${this.apiUrl}${id}`);
  }

  getUsers(active: string, direction: string, pageSize: number, pageIndex: number, search: string | undefined) {
    let params = new HttpParams({encoder: new CustomEncoder()})
    params = params.append('sortBy', active);
    params = params.append('direction', direction);
    params = params.append('limit', pageSize);
    params = params.append('offset', pageIndex);
    if (search) {
      params = params.append('search', search);
    }
    return this.http.get<ListResponse<UserResponse>>(this.apiUrl + "get-users", {params});
  }

  getRoles(): Observable<RoleResponse[]> {
    return this.http.get<RoleResponse[]>(`${this.apiUrl}get-roles`);
  }
}

