import { Component } from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Registration, RegistrationType } from '../models/registration.model';
import { RegistrationService } from '../services/registration.service';
import { debounceTime, distinctUntilChanged, switchMap, filter } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})

export class RegistrationComponent {

  form: FormGroup;
  registrationTypes = Object.values(RegistrationType);
  teamSuggestions: string[] = [];

  registrationTypeLabels: { [key in RegistrationType]: string } = {
    [RegistrationType.Registration]: 'Normale Meldung',
    [RegistrationType.LateRegistration]: 'Nachmeldung',
    [RegistrationType.Reregistration]: 'Ummeldung'
  };

  constructor(private fb: FormBuilder,
              private dataService: RegistrationService,
              private snackBar: MatSnackBar) {
    this.form = this.fb.group({
      type: [RegistrationType.Registration, Validators.required],
      race: ['', Validators.required],
      startNo: ['', Validators.required],
      team: ['', Validators.required],
      chairMan: ['', Validators.required],
      position1: [''],
      position2: [''],
      position3: [''],
      position4: [''],
      position5: [''],
      position6: [''],
      position7: [''],
      position8: [''],
      positionCox: ['']
    });

    this.form.get('team')?.valueChanges
      .pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap(query => query.length >= 3 ? this.dataService.searchTeams(query) : [])
      )
      .subscribe(results => {
        this.teamSuggestions = results.map(r => r.name);
      });
  }


  submit() {
    if (this.form.valid) {
      const registration = this.form.value;
      console.log('Registration ready to submit:', registration);
      this.dataService.addRegistration(registration).subscribe({
        next: res => {
          this.snackBar.open('Meldung wurde eingereicht ✅', 'OK', {
              duration: 3000,
              horizontalPosition: 'right',
              verticalPosition: 'top'
            });
            },
        error: err => {
          alert('Fehler beim Upload: ' + err.error)
          console.error(err.error);
        }
      });
    }
  }

  selectTeam(name: string) {
    this.form.get('team')?.setValue(name);
    this.teamSuggestions = [];
  }

  resetForm() {
    this.form.reset({
      type: RegistrationType.Registration,
      race: '',
      startNo: '',
      team: '',
      chairMan: '',
      position1: '',
      position2: '',
      position3: '',
      position4: '',
      position5: '',
      position6: '',
      position7: '',
      position8: '',
      positionCox: ''
    });
    this.teamSuggestions = [];

    this.snackBar.open('Formular wurde zurückgesetzt ✅', 'OK', {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top'
    });
  }


}
