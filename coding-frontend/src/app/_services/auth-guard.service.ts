import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service'
import { CanActivate } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private auth: AuthService) { }

  canActivate() {
    return this.auth.loggedIn()
  }


}
