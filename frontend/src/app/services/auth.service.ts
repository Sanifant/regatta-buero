import { Injectable } from '@angular/core';
import {UserObject} from "../models/user.model";
import {ReplaySubject} from "rxjs";

@Injectable({ providedIn: 'root' })
export class AuthService {

  private currentUserSource = new ReplaySubject<UserObject | undefined>(1);
  public currentUser$ = this.currentUserSource.asObservable();


  getRoles(): string[] {
    const token = localStorage.getItem('token');
    if (!token) return [];

    const payload = JSON.parse(atob(token.split('.')[1]));
    const roles = payload['role'];
    return Array.isArray(roles) ? roles : [roles];
  }

  isLoggedIn(): boolean {
    const token = !!localStorage.getItem('token');
    if (token) {
      this.setUser(new UserObject());
    }
    return token;
  }

  setUser(userModel: UserObject | undefined) {
    if (!userModel) {
      localStorage.removeItem('token');
    }
    this.currentUserSource.next(userModel);
  }
}
