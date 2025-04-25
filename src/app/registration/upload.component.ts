import { Component } from '@angular/core';
import {CommonModule, NgForOf, NgIf} from '@angular/common';
import {RegistrationService} from "../services/registration.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-upload',
  standalone: true,
  styleUrl: './registration.component.css',
  templateUrl: './upload.component.html',
  imports: [
    NgIf,
    NgForOf
  ]
})
export class UploadComponent {
  teams: any[] = [];
  xmlContent: string | null = null;

  constructor(private dataService : RegistrationService,
              private snackBar: MatSnackBar) {}

  onFileSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.xmlContent = reader.result as string;
      };
      reader.readAsText(file);
    }
  }


  getAllTeams() {
    this.dataService.loadTeams().subscribe(data => this.teams = data);
  }

  uploadFile() {
    if (!this.xmlContent) return;

    this.dataService.uploadFile(this.xmlContent).subscribe({
      next: res => {
        this.snackBar.open('Teams erfolgreich hochgeladen ✅', 'OK', {
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

  deleteTeams() {
    this.dataService.deleteTeams().subscribe({
      next: res => {
        this.snackBar.open('Alle Teams gelöscht ✅', 'OK', {
          duration: 3000,
          horizontalPosition: 'right',
          verticalPosition: 'top'
        });
      },
      error: err => {
        alert('Fehler beim Upload: ' + err)
        console.error(err);
      }
    });
  }
}
