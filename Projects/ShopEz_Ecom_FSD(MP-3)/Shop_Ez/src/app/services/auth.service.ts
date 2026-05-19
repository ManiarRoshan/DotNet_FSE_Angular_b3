import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { RegisterDto, LoginDto, User } from '../models/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:7259/api/auth';
  private tokenKey = 'token';
  private userKey = 'user';

  constructor(private http: HttpClient) {}

  register(dto: RegisterDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, dto);
  }

  login(dto: LoginDto): Observable<string> {
    return this.http.post(`${this.apiUrl}/login`, dto, { responseType: 'text' })
      .pipe(tap(token => this.setTokenAndDecodeUser(token)));
  }

  private setTokenAndDecodeUser(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const userId = payload.userId || payload.nameid || payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || 0;
      const name = payload.unique_name || payload.name || payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || 'User';
      const email = payload.email || payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'] || '';
      const role = payload.role || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || 'Customer';

      const user: User = { userId: Number(userId), name, email, role };
      this.setCurrentUser(user);
    } catch(e) {
      console.error('JWT decode failed', e);
      this.logout();
    }
  }

  setToken(token: string): void { localStorage.setItem(this.tokenKey, token); }
  getToken(): string | null { return localStorage.getItem(this.tokenKey); }

  setCurrentUser(user: User): void {
    localStorage.setItem(this.userKey, JSON.stringify(user));
  }

  getCurrentUser(): User | null {
    const userStr = localStorage.getItem(this.userKey);
    if (userStr) {
      try {
        return JSON.parse(userStr);
      } catch(e) { return null; }
    }
    const token = this.getToken();
    if (token) {
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const userId = payload.userId || payload.nameid || 0;
        const name = payload.unique_name || payload.name || 'User';
        const email = payload.email || '';
        const role = payload.role || 'Customer';
        const user = { userId: Number(userId), name, email, role };
        this.setCurrentUser(user);
        return user;
      } catch(e) { 
        this.logout(); 
        return null; 
      }
    }
    return null;
  }

  isLoggedIn(): boolean { 
    const token = this.getToken();
    if (!token) return false;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      if (payload.exp) {
        const expiry = payload.exp * 1000;
        if (Date.now() >= expiry) {
          this.logout();
          return false;
        }
      }
      return true;
    } catch {
      return false;
    }
  }

  isAdmin(): boolean { 
    return this.getCurrentUser()?.role === 'Admin'; 
  }

  getUserId(): number | null { 
    return this.getCurrentUser()?.userId || null; 
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
  }
}