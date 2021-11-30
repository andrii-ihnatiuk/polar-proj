import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../shared/services/user.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  styles: []
})

export class LoginComponent implements OnInit {
  formModel = {
    UserName: '',
    Password: ''
  }
  constructor(private userService: UserService, private router: Router, private toastr: ToastrService, private translateService: TranslateService) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/profile');
  }

  onSubmit(form: NgForm) {
    this.userService.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        console.log(localStorage.getItem('token'));
        this.router.navigateByUrl('/profile');
      },
      err => {
        if (err.status == 400)
          this.toastr.error('Incorrect username or password.', 'Authentication failed.');
        else
          console.log(err);
      }
    );
  }

  updateLanguage(lan: string): void {
    this.translateService.use(lan);
    localStorage.setItem('lang', lan);
  }

}
