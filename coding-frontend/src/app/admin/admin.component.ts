import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_Services/auth.service';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertifyService } from '../_Services/alertify.service';
import { Router } from '@angular/router';
import { UserService } from '../_Services/user.service';
import { User } from '../_Models/User';
import { JwtHelperService } from '@auth0/angular-jwt';



@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass'],

})
export class AdminComponent implements OnInit {


  model: any = {};
  isAuthenticated = false;
  currentData: User;

  constructor(public auth: AuthService, private alertify: AlertifyService, private router: Router, private user: UserService) { }

  ngOnInit() {
    this.isAuthenticated = this.auth.loggedIn();
    this.getUserData();

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

  getUserData() {
    return this.user.getUserInfo().subscribe((userd: User) => {
      this.currentData = userd;
      console.log(this.currentData);
    }, error => {
      console.log('error' + error.error.message);
    });
  }

  updateUserInfo() {
    return this.user.updateUser(this.currentData).subscribe(next => {
      this.alertify.success('Perfil actualizado ok');
      this.user.announceUserChanged(this.currentData);
    }, error => {
      console.log(error.error.message);
    });
  }


}
