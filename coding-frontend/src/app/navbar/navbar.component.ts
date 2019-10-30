import { environment } from './../../environments/environment';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_Models/User';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit {
  user: any;
  isDataLoading: boolean = false;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getUser(1);
  }

  getUser(id: number) {
    this.http.get(environment.apiUrl + 'Users/' + id).subscribe(
      (response: User) => {
        this.user = response;
        console.log(response);
        this.isDataLoading = true;
      }
    , error => console.log(error));
  }

}