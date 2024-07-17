import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service'
import { CanActivate, Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private auth: AuthService, private router: Router) { }

  canActivate() {
    if (this.auth.loggedIn()) {
      return true
    }
    else {
      this.router.navigate(['/pages/home'])
    }
  }


}
