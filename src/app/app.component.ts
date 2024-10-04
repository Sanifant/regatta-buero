import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ApiService } from './finish.service';
import { NgIf, NgFor } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgIf, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit  {  
  title = 'Zielfotos';

  data: any;

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    console.log("starting OnInit");
    this.apiService.getData().subscribe(response => {
      this.data = response;
    });
  }
}
