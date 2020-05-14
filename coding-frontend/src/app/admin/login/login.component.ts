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


  constructor(private auth: AuthService, private router: Router, private toast: AlertService, private activatedRouter: ActivatedRoute) {
    //   if (this.auth.currentUserValue) { 
    //     this.router.navigate(['/profile']);
    // }
  }

  test() {

  }

  ngOnInit() {
    this.loginStatus = this.activatedRouter.snapshot.data;

    if (this.loginStatus) {
      this.router.navigate(['/profile'])
    }

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
        console.log("luego del toast")
        this.router.navigate(['admin/profile']);
      }
    );
  }

  testSpiner() {
    this.loginSpinner = true
  }



}
