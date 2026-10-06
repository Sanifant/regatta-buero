import { Component } from '@angular/core';
import {ApiService} from "../services/finish.service";
import {LogObject} from "../models/log.model";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-settings',
  imports: [],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})
export class SettingsComponent {

  constructor(private finishService: ApiService,
              private snackBar: MatSnackBar) { }

  deleteFinishPhotos(): void{
    var temp = this.finishService.deleteData().subscribe({
      next: (data: any ) => {
        this.snackBar.open('Bilder wurden gelöscht ✅', 'OK', {
          duration: 3000,
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
      },
      error: (err: any) => {
        alert(err);
      }
    })
  }
}
