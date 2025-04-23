import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { FinishPhotoComponent } from './finish-photo/finish-photo.component';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {LoginComponent} from "./login/login.component";
import {LogviewerComponent} from "./logviewer/logviewer.component";

export const routes: Routes = [
    { path: '',
      redirectTo: '/home',
      pathMatch: 'full'},
    { path: 'home',
      component: HomeComponent },
    { path: 'finish',
      component: FinishPhotoComponent },
    { path: 'registration',
      loadChildren: () => import('./registration/registration.module').then(m => m.RegistrationModule) },
    { path: 'about',
      component: AboutComponent },
  { path: 'login', component: LoginComponent},
  { path: 'logout', component: LoginComponent },
  { path : 'logfiles', component: LogviewerComponent }
];

@NgModule({
    declarations: [

    ],
    imports: [
        RouterModule.forRoot(routes),
        BrowserModule,
        MatSnackBarModule,
        FormsModule],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }

