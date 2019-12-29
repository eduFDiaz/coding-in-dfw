import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../_Models/User';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl + 'users/';

  constructor(private http: HttpClient) { }

  getUserInfo() {
    return this.http.get(this.baseUrl + '1');
  }

  updateUser(user: User) {
    return this.http.put()
  }


}
