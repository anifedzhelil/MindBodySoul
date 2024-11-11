import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AwesomeIconsComponent } from './awesome-icons.component';

describe('AwesomeIconsComponent', () => {
  let component: AwesomeIconsComponent;
  let fixture: ComponentFixture<AwesomeIconsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AwesomeIconsComponent]
    });
    fixture = TestBed.createComponent(AwesomeIconsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
