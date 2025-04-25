import { Injectable } from '@angular/core';
import {UserObject} from "../models/user.model";

@Injectable({ providedIn: 'root' })
export class AuthService {

  currentUser: UserObject | undefined;

  getRoles(): string[] {
    const token = localStorage.getItem('token');
    if (!token) return [];

    const payload = JSON.parse(atob(token.split('.')[1]));
    const roles = payload['role'];
    return Array.isArray(roles) ? roles : [roles];
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  setUser(userModel: UserObject) {
    this.currentUser = userModel;
  }
}
