import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";
import {Component, OnDestroy, OnInit} from "@angular/core";
import {AsyncPipe, NgIf} from "@angular/common";
import {Subscription} from "rxjs";
import {UserObject} from "../models/user.model";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    NgIf,
    AsyncPipe
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})

export class NavbarComponent implements OnInit, OnDestroy {
  roles: string[] = [];
  isLoggedIn: boolean = false;
  userName: string = "";
  private userSubscription: Subscription | undefined;


  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.userSubscription = this.authService.currentUser$.subscribe( (user: UserObject | undefined) => {
      this.isLoggedIn = !!user;
      console.log('user changed to ' + this.isLoggedIn);
    })
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  hasRole(role: string): boolean {
    return this.roles.includes(role);
  }

  logout() {
    this.authService.setUser(undefined);
    this.router.navigate(['/login']);
  }

  ngOnDestroy(): void {
    if (this.userSubscription) {
      this.userSubscription.unsubscribe();
    }
  }
}
