import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient ) { }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.email, Validators.required]],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(6)]],
      Confirm: ['', [Validators.required, Validators.minLength(6)]],
    }, { validator: this.comparePasswords })
  });

  comparePasswords(fb: FormGroup): void {
    const confirmPswrdCtrl = fb.get('ConfirmPassword');
    if (confirmPswrdCtrl?.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password')?.value !== confirmPswrdCtrl?.value) {
        confirmPswrdCtrl?.setErrors({ passwordMismatch: true });
      }
      else {
        confirmPswrdCtrl?.setErrors(null);
      }
    }
  }

  register(): Observable<any> {

    const body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password,
    };
    return this.http.post(`${environment.apiUrl}user/register`, body);
  }

  login(formData: any): Observable<any> {
    return this.http.post(`${environment.apiUrl}user/login`, formData);
  }

  getUserProfile(): Observable<any> {
    var tokenHeader = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('token')});
    return this.http.get(`${environment.apiUrl}user/profile`, {headers: tokenHeader});
  }

  getRating(): Observable<any> {
    return this.http.get(`${environment.apiUrl}home/rating`);
  }
}

