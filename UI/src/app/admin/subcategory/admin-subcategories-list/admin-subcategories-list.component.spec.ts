import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSubcategoriesListComponent } from './admin-subcategories-list.component';

describe('AdminSubcategoriesListComponent', () => {
  let component: AdminSubcategoriesListComponent;
  let fixture: ComponentFixture<AdminSubcategoriesListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminSubcategoriesListComponent]
    });
    fixture = TestBed.createComponent(AdminSubcategoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
