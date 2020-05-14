import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { map, catchError } from 'rxjs/operators';
import { User } from '../_models/User';

import { JwtHelperService } from '@auth0/angular-jwt';
import { Subject, BehaviorSubject, throwError, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<User>

  public currentUser: Observable<User>

  private currentDisplayModeSubject: BehaviorSubject<boolean>

  private currentDisplayMode: Observable<boolean>

  private isLogedIn = new BehaviorSubject(true);

  currentLoginStatus = this.isLogedIn.asObservable();

  jwtHelper = new JwtHelperService();

  apiUrl = 'http://localhost:5050/api/'

  decodedToken: any;


  constructor(private http: HttpClient) {
    // Read the user from the local storage
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('data')));
    this.currentUser = this.currentUserSubject.asObservable();

    this.currentDisplayModeSubject = new BehaviorSubject<boolean>(true)
    this.currentDisplayMode = this.currentDisplayModeSubject.asObservable()
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(model: any) {
    return this.http.post(this.apiUrl + 'auth/login', model).pipe(
      map((response: any, ) => {
        const user = response;
        if (user) {
          console.log(user.user)
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
          localStorage.setItem('data', JSON.stringify(user.user));
          this.currentUserSubject.next(user.user);
          this.changeCurrentLoginStatus(true)

        }
      },
      ), catchError(this.handleError)
    );
  }

  changeCurrentLoginStatus(status: boolean) {
    this.isLogedIn.next(status)
  }

  changeCurrentDisplayMode() {
    this.currentDisplayModeSubject.next(true)
  }

  changeCurrentUser(user: User) {
    this.currentUserSubject.next(user);
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    localStorage.removeItem('token')
    localStorage.removeItem('data')
    this.changeCurrentLoginStatus(false)
    this.currentUserSubject.next(null);
  }

  handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }

  getUser(): Observable<User> {
    return this.currentUserSubject.asObservable()
  }

  getDisplayMode(): Observable<boolean> {
    return this.currentDisplayModeSubject.asObservable()
  }
}



