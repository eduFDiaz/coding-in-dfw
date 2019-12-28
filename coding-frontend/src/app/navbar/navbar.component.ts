import { environment } from './../../environments/environment';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../_Models/User';
import { AlertifyService } from '../_Services/alertify.service';
import { AuthService } from '../_Services/auth.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit {
  user: any;
  isDataLoading = false;
  constructor(private http: HttpClient, private alertify: AlertifyService, private auth: AuthService) { }

  ngOnInit() {
    
  }




}
