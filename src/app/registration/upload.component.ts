import { Component } from '@angular/core';
import {CommonModule, NgForOf, NgIf} from '@angular/common';
import {RegistrationService} from "../services/registration.service";

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

  constructor(private dataService : RegistrationService) {}

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

    this.dataService.uploadFile(this.xmlContent);
  }
}
