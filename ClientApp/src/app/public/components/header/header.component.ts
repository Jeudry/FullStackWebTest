import {Component, Input} from '@angular/core';
import {UserResponse} from "@core/interfaces/userResponse";
import {AuthenticateService} from "@modules/services/authentication.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.sass'
})
export class HeaderComponent {
  @Input() user?: UserResponse;
  openMenu = false;

  constructor(
    private readonly authService: AuthenticateService
  ) {
  }

  logout() {
    this.authService.logout();
  }
}
