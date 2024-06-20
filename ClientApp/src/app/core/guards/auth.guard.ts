import {ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot} from '@angular/router';
import {Injectable} from "@angular/core";
import {config} from "@env/config.dev";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {LoginResponse} from "@core/interfaces/loginResponse";
import {AuthenticateService} from "@modules/services/authentication.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {

  apiUrl = config.apiURL;

  constructor(private authService: AuthenticateService, private router: Router, private http: HttpClient) {
  }

  async canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    const token = localStorage.getItem("token") || '{}';
    if (this.authService.isUserAuthenticated()) {
      return true;
    }

    const isRefreshSuccess = await this.tryRefreshingTokens(token);
    if (!isRefreshSuccess) {
      this.router.navigate([''], {queryParams: {returnUrl: state.url}});
    }
    return isRefreshSuccess;
  }

  private async tryRefreshingTokens(token: string): Promise<boolean> {
    const refreshToken: string = localStorage.getItem("refreshToken") || '{}';
    if (!token || !refreshToken) {
      return false;
    }

    const credentials = JSON.stringify({accessToken: token, refreshToken: refreshToken});
    let isRefreshSuccess: boolean = false;
    const refreshRes = await new Promise<LoginResponse>((resolve, reject) => {
      this.http.post<LoginResponse>(this.apiUrl + "Token/refresh", credentials, {
        headers: new HttpHeaders({
          "Content-Type": "application/json"
        })
      }).subscribe({
        next: (res: LoginResponse) => resolve(res),
        error: (_) => {
          isRefreshSuccess = false;
        }
      });
    });
    return isRefreshSuccess;
  }
}
