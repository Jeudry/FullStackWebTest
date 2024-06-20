import {Component} from '@angular/core';
import {AuthenticateService} from "@modules/services/authentication.service";
import {UserResponse} from "@core/interfaces/userResponse";
import Swal from "sweetalert2";

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.sass'
})
export class LayoutComponent {

  user!: UserResponse;

  constructor(
    private authService: AuthenticateService
  ) {
    this.authService.getCurrentDetailUser().subscribe({
      next: (user) => {
        this.user = user;
      },
      error: (error) => {
        console.log(error);
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: 'Something went wrong!',
        }).then(() => {

        });
      }
    });
  }

}
