import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {
  text: string;

  constructor(private http: HttpClient) {
    this.text = '';
  }

  ngOnInit(): void {
    this.http.get(`${environment.apiUrl}api/test`, { responseType: 'text' }).subscribe((data: string) => this.text = data);
  }

}
