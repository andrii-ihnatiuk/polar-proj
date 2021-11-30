import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  title = 'Polar-client';

  constructor(private router: Router, private translateService: TranslateService) { }

  ngOnInit(): void {
  }

  goTest() {
    this.router.navigate(['/test'])
  }
}
