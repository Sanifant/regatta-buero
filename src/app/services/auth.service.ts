import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  getRoles(): string[] {
    const token = localStorage.getItem('token');
    if (!token) return [];

    const payload = JSON.parse(atob(token.split('.')[1]));
    const roles = payload['role'];
    return Array.isArray(roles) ? roles : [roles];
  }

  isLoggedIn(): boolean {
    return true; //!!localStorage.getItem('token');
  }
}
