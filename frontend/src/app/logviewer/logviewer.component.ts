import {Component, OnInit} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {LoggingService} from "../services/logging.service";
import {LogObject} from "../models/log.model";
import {PagedResult} from "../models/pagedResult.model";
import {Observable} from "rxjs";

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
  logs: LogObject[]= [];
  isLoading = true;
  error?: string;
  currentPage = 1;
  pageSize = 10;
  totalItems = 0;

  constructor(private loggingService: LoggingService) {
  }

  ngOnInit() {
    this.loadData(this.currentPage);
  }

  loadData(page: number) {
    this.loggingService.searchLogs(page, this.pageSize).subscribe({
      next: value => {
        this.logs = value.items;
        this.totalItems = value.totalCount;
        this.currentPage = page;
        this.isLoading = false;
      },
      error: err => {
        this.error = `Fehler beim Laden der Daten. ${err.message}`;
      }
    });
  }

  onPageChange(page: number) {
    this.loadData(page);
  }
}
