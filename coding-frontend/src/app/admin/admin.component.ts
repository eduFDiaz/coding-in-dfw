import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { User } from '../_Models/User';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertifyService } from '../_Services/alertify.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass'],

})
export class AdminComponent implements OnInit {

  model: any = {};
  isAuthenticated = true;

  user: any;

  constructor(public auth: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {

  }

  loggedIn() {
    
  }

}
