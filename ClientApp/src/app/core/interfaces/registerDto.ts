export interface RegisterDto {
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface CreateDto {
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;
  rolesId: string[];
}
