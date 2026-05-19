export interface User {
  userId: number;
  name: string;
  email: string;
  role: string;
}

export interface RegisterDto {
  name: string;
  email: string;
  password: string;
  role: string;
}

export interface LoginDto {
  email: string;
  password: string;
}