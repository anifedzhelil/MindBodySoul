import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSubCategoriesListComponent } from './admin-subcategories-list.component';

describe('AdminSubcategoriesListComponent', () => {
  let component: AdminSubCategoriesListComponent;
  let fixture: ComponentFixture<AdminSubCategoriesListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AdminSubCategoriesListComponent]
    });
    fixture = TestBed.createComponent(AdminSubCategoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
