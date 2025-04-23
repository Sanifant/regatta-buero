import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Registration} from "../models/registration.model";
import {LogObject} from "../models/log.model";

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  private readonly apiUrl = 'https://buero.luebeckregatta.de/api';
  private readonly apiKey = '37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12';

  constructor(private http: HttpClient) {
  }

  uploadFile(teamContent :string) {
    if (teamContent) return;

    const headers = new HttpHeaders({
      'Content-Type': 'application/xml',
      'X-API-KEY': `${this.apiKey}`
    });

    this.http.post(this.apiUrl + '/Team', teamContent, { headers, responseType: 'text' })
      .subscribe({
        next: res => alert('Upload erfolgreich!'),
        error: err => alert('Fehler beim Upload: ' + err.error)
      });
  }

  searchTeams(query: string): Observable<any[]> {
    const params = new HttpParams().set('teamName', query);
    return this.http.get<any[]>(`${this.apiUrl}/Team/select`, { params });
  }

  loadTeams(): Observable<LogObject[]> {

    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });
    return this.http.get<any[]>(this.apiUrl + '/Team', { headers})
  }

  loadRegistration(): Observable<Registration[]> {
    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });
    return this.http.get<any[]>(this.apiUrl + '/Registration', { headers})
  }

  addRegistration(registration: Registration): Observable<void> {
    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });
    return this.http.put<void>(this.apiUrl + '/Registration', registration, { headers});
  }
}
