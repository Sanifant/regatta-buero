import { Component, OnInit, OnDestroy } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiService } from './api.service';
import { NgIf, NgFor } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {


  title = 'testing';

  data: any;
  interval: any;

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    console.log("starting OnInit");

    this.interval = setInterval(() => {
      this.callData(); // api call
    }, 10000);
  }

  callData() {
    this.apiService.getData().subscribe(response => {
      console.log("reloading data");
      this.data = response;
    });

  }

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }
}
