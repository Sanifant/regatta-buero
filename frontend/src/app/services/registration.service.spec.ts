import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

import { RegistrationService } from './registration.service';

describe('RegistrationService', () => {
  let service: RegistrationService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(RegistrationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('uses the shared repository API through the same-origin proxy', () => {
    const http = TestBed.inject(HttpTestingController);
    service.loadRegistration().subscribe(result => expect(result).toEqual([]));
    const request = http.expectOne('/api/Registration');
    expect(request.request.method).toBe('GET');
    request.flush([]);
    http.verify();
  });

});
