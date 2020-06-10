import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Injectable } from '@angular/core';

import { ActivatedRoute } from '@angular/router';

import { AuthService } from '../../_services/auth.service'
import { AlertService } from '../../_services/alert.service'
import { Router } from '@angular/router';

import { User } from '../../_models/User';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  userdata: any = {
    email: '',
    password: ''
  };

  currentUser: User;

  error: any

  loginSpinner = false;
  loginStatus: any

  logintrigger = false

  constructor(private auth: AuthService, private router: Router, private toast: AlertService, private activatedRouter: ActivatedRoute) {

  }

  ngOnInit() {
    if (localStorage.getItem('token')) {
      this.logintrigger = true
    }
    if (this.logintrigger)
      this.router.navigate(['/profile'])

  }

  login() {
    this.loginSpinner = true;
    this.auth.login(this.userdata).subscribe(
      next => { },
      error => {
        this.error = error
        this.toast.showToast('top-right', 'danger', this.error, 'Im sorry :( ')
        this.loginSpinner = false
      },
      () => {
        console.log("ok")
        this.toast.showToast('bottom-left', 'success', 'You have sign in succefully!', 'Welcome back to your dashboard!')
        this.router.navigate(['profile']);
      }
    );
  }

  testSpiner() {
    this.loginSpinner = true
  }



}
