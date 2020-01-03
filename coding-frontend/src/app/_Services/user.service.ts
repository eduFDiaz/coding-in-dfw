import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../_Models/User';
import { Subject } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})


export class UserService {

  private user = new Subject<User>();

  userInfo$ = this.user.asObservable();

  baseUrl = environment.apiUrl + 'users/';

  constructor(private http: HttpClient) { }

  getUserInfo() {
    return this.http.get(this.baseUrl + '1');
  }

  updateUser(user: User) {
    return this.http.put(this.baseUrl + '1', user, httpOptions);
  }

  announceUserChanged(u: User) {
    this.user.next(u);
  }

}
