import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CityRegisterComponent } from './city-register.component';

describe('CityRegisterComponent', () => {
  let component: CityRegisterComponent;
  let fixture: ComponentFixture<CityRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CityRegisterComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CityRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
