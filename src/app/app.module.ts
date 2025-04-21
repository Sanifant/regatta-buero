import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { FinishPhotoComponent } from './finish-photo/finish-photo.component';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'finish', component: FinishPhotoComponent },
    { path: 'registration', component: RegistrationComponent },
    { path: 'about', component: AboutComponent }
];

@NgModule({
    declarations: [
      
    ],
    imports: [
        RouterModule.forRoot(routes),
        BrowserModule,
        FormsModule],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }