import {Injectable} from '@angular/core';
import {config} from "@env/config.dev";
import {Observable, Subject} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {UserResponse} from "@core/interfaces/userResponse";
import {RegisterDto} from "@core/interfaces/registerDto";
import {LogingDto} from "@core/interfaces/logingDto";
import {LoginResponse} from "@core/interfaces/loginResponse";

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
    return this.http.get<UserResponse>(`${this.apiUrl}get-current-user`);
  }

  currentUser(): UserResponse {
    const token = localStorage.getItem("token");
    const decodedToken = this.jwtHelper.decodeToken(token!);
    const user: UserResponse = {
      id: decodedToken.nameid,
      userName: decodedToken.unique_name,
      email: decodedToken.email,
      role: decodedToken.role
    }
    return user;
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
}

