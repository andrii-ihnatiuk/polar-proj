import { ComponentFixture, TestBed } from '@angular/core/testing';
import { UserService } from 'src/app/shared/services/user.service';

import { LoginComponent } from './login.component';

describe('LoginComponent', () => {
  let login: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let userService: UserService;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    
  });


  /* it('should create', () => {
    expect(component).toBeTruthy();
  }); */
});
