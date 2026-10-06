import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

import { ApiService } from './finish.service';

describe('ApiService', () => {
  let service: ApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(ApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('uses the shared repository API through the same-origin proxy', () => {
    const http = TestBed.inject(HttpTestingController);
    service.getData().subscribe(result => expect(result).toEqual([]));
    const request = http.expectOne('/api/Finish');
    expect(request.request.method).toBe('GET');
    request.flush([]);
    http.verify();
  });

});
