import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocmanComponent } from './docman.component';

describe('DocmanComponent', () => {
  let component: DocmanComponent;
  let fixture: ComponentFixture<DocmanComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocmanComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocmanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
