import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";
import {Component, OnInit} from "@angular/core";
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent implements OnInit {
  roles: string[] = [];
  isLoggedIn: boolean = false;
  userName: string = "";

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    //this.roles = this.authService.getRoles();
    this.isLoggedIn = this.authService.isLoggedIn();

  }

  hasRole(role: string): boolean {
    return this.roles.includes(role);
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
