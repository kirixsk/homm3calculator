import { TestBed } from '@angular/core/testing';

import { Homm3Service } from './homm3.service';

describe('Homm3Service', () => {
  let service: Homm3Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Homm3Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
