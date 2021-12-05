import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})

export class RatingComponent implements OnInit {

  userRating: any;

  constructor(private service: UserService) { }

  ngOnInit() {
    this.service.getRating().subscribe(
      res => {
         this.userRating = res;
      },
      err => {
        console.log(err);
      },
    );
  }
}
