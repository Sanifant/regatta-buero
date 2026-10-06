import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";
import {AuthService} from "../services/auth.service";
import {UserObject} from "../models/user.model";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  model = { username: '', password: '' };

  constructor(private http: HttpClient, private router: Router, private authService: AuthService) {}

  login() {
    if (this.model.username === 'buero' && this.model.password === 'wakenitz') {
      localStorage.setItem('token', 'Toiken');

      var userModel = new UserObject();
      this.authService.setUser(userModel);

      this.router.navigate(['/']);
    }
  }
}
