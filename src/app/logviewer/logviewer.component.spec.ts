import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { LogviewerComponent } from './logviewer.component';

describe('LogviewerComponent', () => {
  let component: LogviewerComponent;
  let fixture: ComponentFixture<LogviewerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogviewerComponent],
      providers: [provideHttpClient(), provideHttpClientTesting()]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LogviewerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
