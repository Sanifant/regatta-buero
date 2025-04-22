import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  private readonly apiUrl = 'https://buero.luebeckregatta.de/api/Team';
  private readonly apiKey = '37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12';

  constructor(private http: HttpClient) {
  }

  uploadFile(teamContent :string) {
    if (teamContent) return;

    const headers = new HttpHeaders({
      'Content-Type': 'application/xml',
      'X-API-KEY': `${this.apiKey}`
    });

    this.http.post('api/Team', teamContent, { headers, responseType: 'text' })
      .subscribe({
        next: res => alert('Upload erfolgreich!'),
        error: err => alert('Fehler beim Upload: ' + err.error)
      });
  }

  searchTeams(query: string): Observable<any[]> {
    const params = new HttpParams().set('teamName', query);
    return this.http.get<any[]>(`${this.apiUrl}/select`, { params });
  }


  loadTeams(): Observable<any[]> {

    const headers = new HttpHeaders({
      'apikey': `${this.apiKey}`
    });
    return this.http.get<any[]>(this.apiUrl, { headers})
  }
}
