import { UserService } from './../../shared/services/user.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
  styles: []
})

export class RegistrationComponent implements OnInit {

  constructor(public userService: UserService, private toastr: ToastrService, private translateService: TranslateService) { }

  ngOnInit(): void {
    this.userService.formModel.reset();
  }

  onSubmit(): void {
    this.userService.register().subscribe(
      res => {
        if (res.succeeded) {
          this.userService.formModel.reset();
          this.toastr.success('New user created!', 'Registration successful.');
        }
        else {
          res.errors.forEach((element: { code: any; description: any; }) => {
            switch (element.code) {
              case 'DuplicateUserName':
                this.toastr.error('Username is already taken', 'Registration failed.');
                break;

              default:
                this.toastr.error(element.description, 'Registration failed.');
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
  }

  updateLanguage(lan: string): void {
    this.translateService.use(lan);
    localStorage.setItem('lang', lan);
  }
}
