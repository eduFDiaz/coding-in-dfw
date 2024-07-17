import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';
@Injectable()
export class GuestGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) { }
  canActivate(): Observable<boolean> | Promise<boolean> | boolean {
    return true
  }
}