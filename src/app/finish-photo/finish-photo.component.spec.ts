import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinishPhotoComponent } from './finish-photo.component';

describe('FinishPhotoComponent', () => {
  let component: FinishPhotoComponent;
  let fixture: ComponentFixture<FinishPhotoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinishPhotoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FinishPhotoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
