import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  register(data: FormGroup) {
    var body = {
      Email: data.value.Email,
      UserName: data.value.UserName,
      Password: data.value.Passwords.Password
    };

    return this.http.post(`${environment.apiUrl}user/register`, body);
  }
}
