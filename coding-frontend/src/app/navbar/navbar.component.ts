import { environment } from './../../environments/environment';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserService } from '../_Services/user.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit {
  userData: any;
  isDataLoading = false;
  constructor(private user: UserService) { }

  ngOnInit() {
    this.getUserInfo();
  }

  getUserInfo() {
    return this.user.getUserInfo().subscribe((data) => {
      this.userData = data;
      console.log(data);
    }, error => {
      console.log(error);
    });

  }


}
