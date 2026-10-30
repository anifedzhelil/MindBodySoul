import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UrlHandleComponent } from './url-handle.component';

describe('UrlHandleComponent', () => {
  let component: UrlHandleComponent;
  let fixture: ComponentFixture<UrlHandleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UrlHandleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UrlHandleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
