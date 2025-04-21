import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Registration, RegistrationType } from '../models/registration.model';
import { RegistrationService } from '../services/registration.service';
import { HttpClient } from '@angular/common/http';
import { debounceTime, distinctUntilChanged, switchMap, filter } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  imports: [
    CommonModule,
    FormsModule
  ],
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})

export class RegistrationComponent {
  
  registrationTypes = Object.values(RegistrationType);
  newRegistration: Registration = new Registration();
  searchTerm = '';
  results: any[] = [];
  private searchSubject = new Subject<string>();


  constructor() {
    this.searchSubject.pipe(
      filter(term => term.length >= 3),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term => RegistrationService.searchTrainer(term))
    ).subscribe(data => this.results = data);

  }

  onSearchChange(term: string) {
    this.searchSubject.next(term);
  }

  onSubmit() {
  }

  onReset() {
    this.newRegistration = new Registration();
  }

  get jsonData() {
    return JSON.stringify(this.newRegistration, null, 2);
  }
}
