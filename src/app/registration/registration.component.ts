import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';

enum RegistrationType {
  Registration = "Registration",
  LateRegistration = "LateRegistration",
  Reregistration = "Reregistration"
}

interface RegistrationObject {
  type: RegistrationType;
  race: string;
  startNo: string;
  team: string;
  position1?: string;
  position2?: string;
  position3?: string;
  position4?: string;
  position5?: string;
  position6?: string;
  position7?: string;
  position8?: string;
  positionCox?: string;
  chairMan: string;
  requestedAt: Date;
}

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})

export class RegistrationComponent  implements OnInit {
  
  registrationForm: FormGroup;
  registrationTypes = Object.values(RegistrationType);

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.registrationForm = this.fb.group({
      type: [RegistrationType.Registration, Validators.required],
      race: ['', Validators.required],
      startNo: ['', Validators.required],
      team: ['', Validators.required],
      position1: [''],
      position2: [''],
      position3: [''],
      position4: [''],
      position5: [''],
      position6: [''],
      position7: [''],
      position8: [''],
      positionCox: [''],
      chairMan: ['', Validators.required],
      requestedAt: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      const registration: RegistrationObject = this.registrationForm.value;
      console.log('Registration Object:', registration);
    } else {
      console.error('Form is invalid');
    }
  }
}
