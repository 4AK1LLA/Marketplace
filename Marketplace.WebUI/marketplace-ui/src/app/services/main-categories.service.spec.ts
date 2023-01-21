import { TestBed } from '@angular/core/testing';

import { MainCategoriesService } from './main-categories.service';

describe('MainCategoriesService', () => {
  let service: MainCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MainCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
