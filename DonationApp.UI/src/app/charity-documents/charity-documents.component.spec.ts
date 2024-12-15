import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharityDocumentsComponent } from './charity-documents.component';

describe('CharityDocumentsComponent', () => {
  let component: CharityDocumentsComponent;
  let fixture: ComponentFixture<CharityDocumentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CharityDocumentsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CharityDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
