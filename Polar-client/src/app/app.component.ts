import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private translateService: TranslateService) { }

  title = 'Polar-client';

  ngOnInit() {
    this.translateService.langs = ['en', 'ukr'];
    this.translateService.setDefaultLang('ukr');
    this.translateService.use(localStorage.getItem('lang') || '');
   }

}
