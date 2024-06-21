import {RoleResponse} from "@core/interfaces/RoleResponse";

export interface UserResponse {
  id: string,
  userName: string,
  email: string,
  roles: RoleResponse[]
}
