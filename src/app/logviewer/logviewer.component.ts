import {Component, OnInit} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {LoggingService} from "../services/logging.service";
import {LogObject} from "../models/log.model";

@Component({
  selector: 'app-logviewer',
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './logviewer.component.html',
  styleUrl: './logviewer.component.css'
})
export class LogviewerComponent implements OnInit{
  logs: LogObject[] = [];
  isLoading = true;
  error?: string;
  private loggingService: any;

  constructor(logingService: LoggingService) {
    this.loggingService = logingService;
  }

  ngOnInit(): void {
    this.loadLogs();
  }

  loadLogs() {

    this.loggingService.loadLogs().subscribe({
      next: (data: LogObject[] ) => {
        this.logs = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.error = 'Fehler beim Laden der Daten.';
        console.error(err);
        this.isLoading = false;
      }
    })
  }
}
