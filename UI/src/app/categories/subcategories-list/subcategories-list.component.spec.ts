import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubCategoriesListComponent } from './subcategories-list.component';

describe('SubcategoriesListComponent', () => {
  let component: SubCategoriesListComponent;
  let fixture: ComponentFixture<SubCategoriesListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SubCategoriesListComponent]
    });
    fixture = TestBed.createComponent(SubCategoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
