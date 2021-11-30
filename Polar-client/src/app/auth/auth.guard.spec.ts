import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';

import { AuthGuard } from './auth.guard';

describe('AuthGuardGuard', () => {
  let guard: AuthGuard;
  let router: Router;

  beforeEach(() => {
    router = jasmine.createSpyObj(['navigate']);
    guard = new AuthGuard(router);
  });


 /*  it('should be created', () => {
    expect(guard).toBeTruthy();
  }); */
});
