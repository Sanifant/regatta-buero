import {Component, OnInit} from "@angular/core";
import {RegistrationService} from "../services/registration.service";
import {Registration} from "../models/registration.model";
import {Observable} from "rxjs";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-registrationlist',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './registrationlist.component.html',
  styleUrl: './registration.component.css'
})

export class RegistrationListComponent implements OnInit{
  registrations: Registration[] = [];
  isLoading = true;
  error?: string;

  constructor(private registrationService: RegistrationService) {

  }

  ngOnInit(): void {
    this.registrationService.loadRegistration().subscribe({
      next: (data) => {
        this.registrations = data;
        this.isLoading = false;
      },
      error: (err) => {
        this.error = 'Fehler beim Laden der Daten.';
        console.error(err);
        this.isLoading = false;
      }
    });
    }
}
