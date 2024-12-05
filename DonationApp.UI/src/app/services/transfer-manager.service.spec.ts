import { TestBed } from '@angular/core/testing';

import { TransferManagerService } from './transfer-manager.service';

describe('TransferManagerService', () => {
  let service: TransferManagerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransferManagerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
