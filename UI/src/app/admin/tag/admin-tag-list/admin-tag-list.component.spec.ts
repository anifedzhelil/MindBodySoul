import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminTagListComponent } from './admin-tag-list.component';

describe('AdminTagListComponent', () => {
  let component: AdminTagListComponent;
  let fixture: ComponentFixture<AdminTagListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminTagListComponent]
    });
    fixture = TestBed.createComponent(AdminTagListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
