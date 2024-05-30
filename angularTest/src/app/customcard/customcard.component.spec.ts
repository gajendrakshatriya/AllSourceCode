import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomcardComponent } from './customcard.component';

describe('CustomcardComponent', () => {
  let component: CustomcardComponent;
  let fixture: ComponentFixture<CustomcardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomcardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomcardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
