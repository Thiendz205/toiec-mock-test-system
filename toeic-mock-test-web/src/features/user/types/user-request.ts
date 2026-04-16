export interface CreateUserRequest {
  name: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  roleId: string;
}

export interface UpdateProfileRequest {
  firstName: string;
  lastName: string;
}

export interface UpdateUserRequest {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  roleId: string;
}