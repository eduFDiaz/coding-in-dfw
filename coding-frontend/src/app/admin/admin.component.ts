import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { User } from '../_Models/User';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertifyService } from '../_Services/alertify.service';
import { Router } from '@angular/router';

import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass'],

})
export class AdminComponent implements OnInit {

  model: any = {};
  isAuthenticated = false;

  user: User;

  constructor(public auth: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.isAuthenticated = this.auth.loggedIn();
  }

  login() {
    this.auth.login(this.model).subscribe(
      next => {
        this.alertify.success('Welcome');
        this.isAuthenticated = true;
      },
      error => {
        this.alertify.error('Errpr' + error.error.message);
      },
    );
  }

  isLoggedin() {
    return !this.auth.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.auth.decodedToken = null;
    this.auth.currentUser = null;
    // tslint:disable-next-line: quotemark
    this.alertify.message('Loged out');
    this.router.navigate(['/home']);
  }

}
