import { TestBed } from '@angular/core/testing';

import { SubCategoryService } from './subcategory.service';

describe('SubcategoryService', () => {
  let service: SubCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
