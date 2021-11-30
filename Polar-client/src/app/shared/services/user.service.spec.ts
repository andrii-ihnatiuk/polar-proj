import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from './user.service';
import { Observable, of } from 'rxjs';

describe('UserService', () => {
  let service: UserService;
  const fb = new FormBuilder();
  let http: HttpClient;
  mockData: FormData;
  
  beforeEach(() => {
    http = jasmine.createSpyObj(['post']);
    service = new UserService(fb, http);
    
    const mockedGoodResponse = {
      "succeeded": "true",
      "error": []
    };
    
    const observable: Observable<Object> = of(mockedGoodResponse);
    service.login = jasmine.createSpy().and.callFake(() => {
      return observable;
    });
  });

  /* it('Надіслана форма повертає повідомлення про успіх (TC9)', () => {
    expect(service.register).toEqual(mockedGoodResponse);
  }); */

  // testing the validity of a login field
  it('Форма з порожнім полем "логін" не може бути валідною (TC2)', () => {
    const loginField = service.formModel.controls.UserName;
    expect(loginField.valid).toBeFalsy();
  });

  // testing the validity of a password field
  it('Форма з порожнім полем "пароль" не може бути валідною (TC3)', () => {
    const passwordField = service.formModel.get('Passwords.Password');
    expect(passwordField?.valid).toBeFalsy();
  });

  // testing the validity of a form if it is not filled
  it('Порожня форма не може бути валідною (TC4)', () => {
    expect(service.formModel.valid).toBeFalsy();
  });

  // testing the validity of a form with incorrect email
  it('Форма з некоректною поштою не може бути валідною (TC7)', () => {
    let errors: any = {};
    service.formModel.controls['Email'].setValue('incorrectemail');
    errors = service.formModel.controls['Email']?.errors || {};
    expect(errors['email']).toBeTruthy();
  });

  // testing the validity of a form with incorrect email
  it('Форма з занадто коротким паролем (<6 символів) не може бути валідною (TC8)', () => {
    let errors: any = {};
    service.formModel.get('Passwords.Password')?.setValue('12');
    errors = service.formModel.get('Passwords.Password')?.errors || {};
    expect(errors['minlength']).toBeTruthy();
  });

  it('Форма з некоректною поштою не може бути валідною (TC11)', () => {
    let errors: any = {};
    service.formModel.controls['Email'].setValue('wrongemail');
    errors = service.formModel.controls['Email']?.errors || {};
    expect(errors['email']).toBeTruthy();
  });

  /* it('Форма з паролем, який не містить строчну літеру, цифру та спецсимвол, не може бути валідною (TC12)', () => {
    let errors: any = {};
    service.formModel.get('Passwords.Password')?.setValue('testpass');
    errors = service.formModel.controls['Email']?.errors || {};
    expect(errors['pattern']).toBeTruthy();
  }); */

  it('Форма із порожнім логіном не може бути валідною (TC13)', () => {
    let errors: any = {};
    let userName = service.formModel.controls['UserName'];
    errors = userName.errors || {};
    expect(errors['required']).toBeTruthy();
  });

  it('Форма із порожнім паролем не може бути валідною (TC14)', () => {
    let errors: any = {};
    let password = service.formModel.get('Passwords.Password');
    errors = password?.errors || {};
    expect(errors['required']).toBeTruthy();
  });

  it('Форма із порожнім іменем користувача не може бути валідною (TC15)', () => {
    let errors: any = {};
    let userName = service.formModel.controls['UserName'];
    errors = userName?.errors || {};
    expect(errors['required']).toBeTruthy();
  });

  it('Форма без логіна та пароля не може бути валідною (TC16)', () => {
    let errors_name: any = {};
    let errors_password: any = {};
    let userName = service.formModel.controls['UserName'];
    let password = service.formModel.get('Passwords.Password');
    errors_name = userName?.errors || {};
    errors_password = password?.errors || {};
    expect(errors_password['required'] && errors_name['required']).toBeTruthy();
  });

  it('Форма без логіну та імені користувача не може бути валідною (TC17)', () => {
    let errors_email: any = {};
    let errors_name: any = {};
    let userName = service.formModel.controls['UserName'];
    let email = service.formModel.controls['Email'];
    errors_name = userName?.errors || {};
    errors_email =  email?.errors || {};
    expect(errors_email['required'] && errors_name['required']).toBeTruthy();
  });


/*   it('Форма без логіну та паролю не може бути валідною (TC18)', () => {
    let errors_login: any = {};
    let errors_password: any = {};
    let password = service.formModel.get('Passwords.Password');
    let email = service.formModel.controls['Email'];
    errors_login = email?.errors || {};
    errors_password =  password?.errors || {};
    expect(errors_login['required'] && errors_password['required']).toBeTruthy();
  });

  it('Порожня форма не може бути валідною (TC19)', () => {
    let errors_login: any = {};
    let errors_password: any = {};
    let errors_name: any = {};
    let password = service.formModel.get('Passwords.Password');
    let email = service.formModel.controls['Email'];
    let userName = service.formModel.controls['UserName'];
    errors_login = email?.errors || {};
    errors_password =  password?.errors || {};
    errors_name = userName?.errors || {};
    expect(errors_login['required'] && errors_password['required']) && errors_name['required'].toBeTruthy();
  });

  it('Імя користувача з кирилицею не підтримується (TC20)', () => {
    let errors_name: any = {};
    service.formModel.controls['UserName'].setValue('Користувач22');
    let userName = service.formModel.controls['UserName'];
    errors_name = userName?.errors || {};
    expect(errors_name['pattern'].toBeTruthy());
  });
 */

/*   it('Форма з занадто довгим логіном (>320 символів) не може бути валідною (TC21)', () => {
    let errors_email: any = {};
    service.formModel.controls['Email'].setValue('verylongemail222222222222222222222222222222222222222222222222222222222222');
    let userName = service.formModel.controls['UserName'];
    errors_email = userName?.errors || {};
    expect(errors_email['maxlength'].toBeTruthy());
  });

  it('Форма із занадто довгим паролем (>32 символів) не може бути валідною (TC22)', () => {
    let errors_password: any = {};
    service.formModel.get('Passwords.Password')?.setValue('VeryLongPassword2222222222222222222222222222222222222222222222222222222222');
    let password = service.formModel.get('Passwords.Password');
    errors_password = password?.errors || {};
    expect(errors_password['maxlength'].toBeTruthy());
  });

  it('Форма із занадто довгим іменем користувача (>32 символів) не може бути валідною (TC23)', () => {
    let errors_name: any = {};
    service.formModel.controls['UserName'].setValue('VeryLongUseName2222222222222222222222222222222222222222222222222222222222');
    let userName = service.formModel.get('Passwords.Password');
    errors_name = userName?.errors || {};
    expect(errors_name['maxlength'].toBeTruthy());
  });
 */


  















  /* it('should be created', () => {
    expect(service).toBeTruthy();
  }); */
});
function mockedGoodResponse(mockedGoodResponse: any) {
  throw new Error('Function not implemented.');
}

