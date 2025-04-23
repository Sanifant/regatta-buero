import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import { Component } from '@angular/core';
import {FormsModule} from "@angular/forms";

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

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    this.http.post<any>('https://localhost:5001/api/auth/login', this.model)
      .subscribe({
        next: res => {
          localStorage.setItem('token', res.token);
          this.router.navigate(['/dashboard']);
        },
        error: err => alert('Login fehlgeschlagen')
      });
  }
}
