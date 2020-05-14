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

  // The service will use a behavior subject so any component can subscribe to changes emitted by it
  photoUrl = new BehaviorSubject<string>('https://www.bsn.eu/wp-content/uploads/2016/12/user-icon-image-placeholder-300-grey.jpg');
  currentPhotoUrl = this.photoUrl.asObservable();

  private currentUserSubject: BehaviorSubject<User>

  public currentUser: Observable<User>

  private isLogedIn = new BehaviorSubject(true);

  currentLoginStatus = this.isLogedIn.asObservable();

  jwtHelper = new JwtHelperService();

  apiUrl = 'http://localhost:5050/api/'

  decodedToken: any;


  constructor(private http: HttpClient) {
    // Read the user from the local storage
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('data')));
    this.currentUser = this.currentUserSubject.asObservable();

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


  changeMemberPhoto(photoUrl: string) {
    // localStorage.setItem('user', JSON.stringify(this.loggedInUser));
    this.photoUrl.next(photoUrl);
  }
}


