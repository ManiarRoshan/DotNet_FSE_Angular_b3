import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface UserAdmin {
  userId: number;
  name: string;
  email: string;
  role: string;
}

@Injectable({ providedIn: 'root' })
export class UserAdminService {
  private apiUrl = `${environment.apiUrl}/users`;

  constructor(private http: HttpClient) {}

  getUsers(): Observable<UserAdmin[]> {
    return this.http.get<UserAdmin[]>(this.apiUrl);
  }

  toggleRole(userId: number): Observable<{ userId: number; role: string; message: string }> {
    return this.http.put<{ userId: number; role: string; message: string }>(
      `${this.apiUrl}/${userId}/toggle-role`,
      {}
    );
  }
}
