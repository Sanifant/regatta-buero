import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Finish} from "../models/finish.model";


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private readonly apiUrl = 'https://buero.luebeckregatta.de/api/Finish';
  private readonly apiKey = '37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12';

  constructor(private http: HttpClient) { }

  getData(): Observable<Finish[]> {
    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });
    return this.http.get<Finish[]>(this.apiUrl, { headers });
  }

  deleteData() : Observable<Object> {
    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });
    return this.http.delete(this.apiUrl, { headers });

  }
}
