import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Registration} from "../models/registration.model";
import {LogObject} from "../models/log.model";
import {PagedResult} from "../models/pagedResult.model";

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  private readonly apiUrl = 'https://buero.luebeckregatta.de/api';
  private readonly apiKey = '37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12';

  constructor(private http: HttpClient) {
  }

  loadLogs(): Observable<LogObject[]> {

    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });
    return this.http.get<any[]>(this.apiUrl + '/Log', { headers})
  }

  searchLogs(page: number, pageSize: number): Observable<PagedResult<LogObject>> {

    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });

    return this.http.get<PagedResult<LogObject>>(this.apiUrl + `/Log/search?page${page}&pagesize=${pageSize}`, { headers})
  }

  addRegistration(log: LogObject): Observable<void> {
    const headers = new HttpHeaders({
      'X-API-KEY': `${this.apiKey}`
    });
    return this.http.put<void>(this.apiUrl + '/Log', log, { headers});
  }
}
