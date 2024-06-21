export interface LoginResponse {
  isAuthSuccessful: boolean;
  expiration: string | null;
  error: number | null;
  token: string | null;
  message: string | null;
}
