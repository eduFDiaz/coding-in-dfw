import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { User } from '../_Models/User';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + localStorage.getItem('token')
  })
};

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
    return this.http.put(this.baseUrl + '1', user, httpOptions);
  }

}
