import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { UserService } from 'src/app/shared/services/user.service';

import { ProfileComponent } from './profile.component';

describe('ProfileComponent', () => {
  let profile: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;
  let router: Router;
  let userService: UserService;
  const testProfile = { login: 'UserTest', password: 'test-pass' };
  

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    router = jasmine.createSpyObj(['navigate']);
    userService = jasmine.createSpyObj(['userService']);
    profile = new ProfileComponent(router, userService);
    const observable: Observable<Object> = of(testProfile);
    userService.getUserProfile = jasmine.createSpy().and.callFake(() => {
      return observable;
    });
    profile.ngOnInit();
  });

  it('Сторінка повинна відображати отримані дані профілю (TC24)', () => {
    expect(profile.userDetails).toEqual(testProfile);
  });

 /*  it('should create', () => {
    expect(component).toBeTruthy();
  }); */
});
