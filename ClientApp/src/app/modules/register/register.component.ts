import {Component} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from "@angular/forms";
import {AuthenticateService} from "@modules/services/authentication.service";
import {Router} from "@angular/router";
import Swal from "sweetalert2";
import {RegisterDto} from "@core/interfaces/registerDto";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.sass'
})
export class RegisterComponent {
  registerForm: FormGroup;
  userName: FormControl = new FormControl(
    '',
    [Validators.required, Validators.minLength(3), Validators.maxLength(256)]
  );
  email: FormControl = new FormControl(
    '',
    [Validators.required, Validators.email, Validators.minLength(3), Validators.maxLength(256)]
  );
  password: FormControl = new FormControl(
    '',
    [Validators.required, Validators.minLength(8), Validators.maxLength(256)]
  );
  confirmPassword: FormControl = new FormControl(
    '',
    [Validators.required, Validators.minLength(8), Validators.maxLength(256)]
  );
  protected readonly confirm = confirm;

  constructor(
    private readonly fb: FormBuilder,
    private readonly authService: AuthenticateService,
    private readonly router: Router
  ) {
    this.registerForm = fb.group({
      userName: this.userName,
      password: this.password,
      email: this.email,
      confirmPassword: this.confirmPassword
    })
  }

  passwordValidation() {
    if (this.password.valid) {
      if (!/\d/.test(this.password.value)) {
        const err: ValidationErrors = {
          ["digits"]: true
        };
        this.password.setErrors(err);
      }
      if (this.password.value !== this.confirmPassword.value) {
        const err: ValidationErrors = {
          ["match"]: true
        };
        this.confirmPassword.setErrors(err);
      }
    }
  }

  onSubmit() {
    this.router.navigate(['']);
  }

  onRegister(formValue: any) {
    const value = {...formValue}

    const registerDto: RegisterDto = {
      userName: value.userName.trim(),
      email: value.email.trim(),
      password: value.password.trim(),
      confirmPassword: value.confirmPassword.trim()
    }

    this.authService.register(registerDto).subscribe({
      next: () => {
        Swal.fire({
          title: 'Success',
          text: 'Register successful, you can now login',
          icon: 'success'
        }).then(() => {
          this.router.navigate(['/login'])
        });
      },
      error: (err) => {
        console.error(err);
        Swal.fire({
          title: 'Error',
          html: err,
          icon: "error"
        }).then(() => {

        });
      }
    })
  }
}
