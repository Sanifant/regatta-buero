import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface finishObject {
  name: string;
  path: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'https://buero.luebeckregatta.de/api/Finish'; // Replace with your API URL
  private apiKey = '37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12'; // Replace with your API key

  constructor(private http: HttpClient) { }

  getData(): Observable<finishObject[]> {
    const headers = new HttpHeaders({
      'apikey': `${this.apiKey}`
    });
    return this.http.get<finishObject[]>(this.apiUrl, { headers });
  }
}
