import { TestBed } from '@angular/core/testing';

import { ArticleVisitsService } from './article-visits.service';

describe('ArticleVisitsService', () => {
  let service: ArticleVisitsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ArticleVisitsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
