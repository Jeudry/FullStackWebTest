import {Component} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import {AuthenticateService} from "@modules/services/authentication.service";
import {LogingDto} from "@core/interfaces/logingDto";
import Swal from "sweetalert2";
import {Router} from "@angular/router";
import {LoginResponse} from "@core/interfaces/loginResponse";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.sass'
})
export class LoginComponent {

  loginForm: FormGroup;
  userName: FormControl = new FormControl(
    '',
    [Validators.required, Validators.minLength(3), Validators.maxLength(256)]
  );
  password: FormControl = new FormControl(
    '',
    [Validators.required, Validators.minLength(8), Validators.maxLength(256)]
  );

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthenticateService,
    private readonly router: Router
  ) {
    this.loginForm = fb.group({
      userName: this.userName,
      password: this.password
    })
  }

  passwordValidation() {
    if (this.password.valid && !/\d/.test(this.password.value)) {
      const err: ValidationErrors = {
        ["digits"]: true
      };
      this.password.setErrors(err);
    }
  }

  onRegister() {
    this.router.navigate(['/register']);
  }

  onSubmit(formValue: any) {
    const value = {...formValue}

    const loginDto: LogingDto = {
      userName: value.userName.trim(),
      password: value.password.trim()
    }

    this.authService.login(loginDto).subscribe({
      next: (res: LoginResponse) => {
        Swal.fire({
          title: 'Success',
          text: 'Login Success',
          icon: 'success'
        }).then(() => {
          localStorage.setItem("token", res.token!);
          localStorage.setItem('expiration', res.expiration!);
          this.authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
          this.router.navigate(['home/products']);
        });
      },
      error: (err) => {
        console.error(err);
        Swal.fire({
          title: 'Login Failed',
          html: err !== "" ? err : "Something went wrong!",
          icon: 'error'
        }).then(() => {

        });
      }
    })
  }
}
