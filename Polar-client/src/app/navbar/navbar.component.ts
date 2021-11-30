import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private translateService: TranslateService) { }

  ngOnInit(): void {
  }

  updateLanguage(lan: string): void {
    this.translateService.use(lan);
    localStorage.setItem('lang', lan);
  }

}
