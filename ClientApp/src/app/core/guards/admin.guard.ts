import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {Injectable} from "@angular/core";
import {AuthenticateService} from "@modules/services/authentication.service";
import Swal from "sweetalert2";

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private authService: AuthenticateService, private router: Router) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authService.isUserAdmin())
      return true;

    Swal.fire({
      icon: 'error',
      title: 'Access Denied',
      text: 'You are not authorized to access this page',
    }).then(() => {
      this.router.navigate(['home'], {queryParams: {returnUrl: state.url}});
    });
    return false;
  }
}

