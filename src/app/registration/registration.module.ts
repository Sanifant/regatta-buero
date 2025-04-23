import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { UploadComponent } from './upload.component';
import { RegistrationComponent } from "./registration.component";
import {RegistrationListComponent} from "./registrationlist.component";

const routes: Routes = [
  { path: '',
    component: RegistrationComponent},
  { path: 'upload',
    component: UploadComponent },
  { path: 'list',
    component: RegistrationListComponent }
];

@NgModule({
  declarations: [

  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forChild(routes),
    UploadComponent,
    RegistrationComponent
  ]
})
export class RegistrationModule { }
