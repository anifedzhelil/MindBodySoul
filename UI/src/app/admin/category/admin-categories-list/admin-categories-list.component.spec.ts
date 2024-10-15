import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCategoriesListComponent } from './admin-categories-list.component';

describe('AdminCategoriesListComponent', () => {
  let component: AdminCategoriesListComponent;
  let fixture: ComponentFixture<AdminCategoriesListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminCategoriesListComponent]
    });
    fixture = TestBed.createComponent(AdminCategoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
