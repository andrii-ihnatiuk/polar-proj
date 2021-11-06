import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  form:FormGroup;
  isOk: boolean;
  responseInfo: string | null;

  constructor(private fb: FormBuilder, private service: UserService) {
    this.form = this.fb.group({
      Email: ['', Validators.email],
      UserName: ['', Validators.required],
      Passwords: this.fb.group({
        Password: ['', [Validators.required, Validators.minLength(6)]],
        ConfirmPassword: ['', Validators.required]
      })
    });
    this.isOk = false;
    this.responseInfo = null;
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.service.register(this.form).subscribe(
      (res: any) => {
        if (res.succeeded) {
            this.form.reset();
            this.responseInfo = 'Everything is okay!';
        } else {
          res.errors.forEach((error: any) => {
            switch (error.code) {
              case 'DuplicateUserName':
                console.log(error.description);
                this.responseInfo = error.description;
                break;
            
              default:
                this.responseInfo = 'Unkown error';
                console.log('Unknown error');
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    )
    
    return null;
  }

}
