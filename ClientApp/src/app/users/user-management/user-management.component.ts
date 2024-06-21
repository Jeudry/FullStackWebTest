import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ValidationErrors, Validators} from "@angular/forms";
import {ActivatedRoute} from "@angular/router";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import Swal from "sweetalert2";
import {AuthenticateService} from "@modules/services/authentication.service";
import {CreateDto} from "@core/interfaces/registerDto";
import {UserDto} from "@core/interfaces/userDto";
import {lastValueFrom} from "rxjs";
import {RoleResponse} from "@core/interfaces/RoleResponse";
import {InputType} from "@public/components/input/input.component";

export interface OptionsList {
  value: string;
  label: string;
}


@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.sass'
})
export class UserManagementComponent implements OnInit {
  title: string = '';
  userId?: string;
  rolesList: OptionsList[] = [];

  managementForm!: FormGroup;
  userName: FormControl = new FormControl(
    '',
    [Validators.required, Validators.minLength(3), Validators.maxLength(256)]
  );
  email: FormControl = new FormControl(
    '',
    [Validators.minLength(5), Validators.maxLength(1024)]
  );
  password: FormControl = new FormControl(
    null,
    [Validators.required, Validators.minLength(8), Validators.maxLength(256)]
  );
  confirmPassword: FormControl = new FormControl(
    null,
    [Validators.required, Validators.minLength(8), Validators.maxLength(256)]
  );
  rolesId: FormControl = new FormControl(
    [],
    [Validators.required]
  );
  protected readonly InputType = InputType;

  constructor(
    private readonly fb: FormBuilder,
    private readonly route: ActivatedRoute,
    readonly dialogRef: MatDialogRef<UserManagementComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: any,
    private readonly usersService: AuthenticateService
  ) {

  }

  async ngOnInit(): Promise<void> {
    await this.initializeForm();
  }

  onClose() {
    this.dialogRef.close();
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

  async initializeForm() {
    this.managementForm = this.fb.group({
      userName: this.userName,
      email: this.email,
      rolesId: this.rolesId
    });

    this.userId = this._data.id;
    await this.getRoles();
    if (this.userId) {
      this.title = 'Edit user';
      this.getuser(this.userId);
    } else {
      this.managementForm.addControl('password', this.password);
      this.managementForm.addControl('confirmPassword', this.confirmPassword);
      this.title = 'Create user';
    }
  }

  async getRoles() {
    const response = this.usersService.getRoles();
    const roles = await lastValueFrom(response);
    this.rolesList = roles.map((role: RoleResponse) => {
      return {
        value: role.id,
        label: role.name
      }
    });
  }

  getuser(userId: string) {
    this.usersService.getUser(userId).subscribe({
      next: (user) => {
        this.managementForm.patchValue(user);
        if (user.roles.length > 0)
          this.rolesId.setValue(user.roles[0].id);
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  onSubmit(formValue: any) {
    const value = {...formValue}

    if (this.userId) {
      this.updateuser(value);
    } else {
      this.createuser(value);
    }
  }

  createuser(userValue: any) {
    const userDto: CreateDto = {
      userName: userValue.userName,
      email: userValue.email,
      password: userValue.password,
      confirmPassword: userValue.confirmPassword,
      rolesId: [userValue.rolesId]
    }
    this.usersService.createUser(userDto).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'user created',
        }).then(() => {
          this.dialogRef.close(true);
        });
      }, error: (err) => {
        console.error(err);
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          html: err
        }).then(() => {

        });
      }
    })
  }

  updateuser(userValue: any) {
    const userDto: UserDto = {
      userName: userValue.userName,
      email: userValue.email,
      rolesId: userValue.rolesId
    }

    this.usersService.updateUser(this.userId!, userDto).subscribe({
      next: () => {
        Swal.fire({
          icon: 'success',
          title: 'user updated'
        }).then(() => {
          this.dialogRef.close(true);
        });
      }, error: (err) => {
        console.error(err);
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          html: err
        }).then(() => {

        });
      }
    })
  }
}
