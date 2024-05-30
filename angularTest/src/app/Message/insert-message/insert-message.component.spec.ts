import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertMessageComponent } from './insert-message.component';

describe('InsertMessageComponent', () => {
  let component: InsertMessageComponent;
  let fixture: ComponentFixture<InsertMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsertMessageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(InsertMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
