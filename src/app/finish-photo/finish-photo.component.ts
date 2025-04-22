import {Component, OnInit} from '@angular/core';
import {ApiService} from "../services/finish.service";
import {NgForOf, NgOptimizedImage} from "@angular/common";
import {Finish} from "../models/finish.model";

@Component({
  selector: 'app-finish-photo',
  standalone: true,
  imports: [
    NgForOf,
    NgOptimizedImage
  ],
  templateUrl: './finish-photo.component.html',
  styleUrl: './finish-photo.component.css'
})
export class FinishPhotoComponent implements OnInit {

  data: Finish[] | undefined;

  constructor(private finishService: ApiService) {
  }

  ngOnInit() {
    console.log("starting OnInit");
    setInterval(() => {
      console.log("refreshing data");
      this.finishService.getData().subscribe({
        next: response => (this.data = response),
        error: err => console.log(err)
    })
    }, 5000);
  }
}
