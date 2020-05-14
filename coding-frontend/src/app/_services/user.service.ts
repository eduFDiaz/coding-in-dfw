import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


import { Observable, throwError } from 'rxjs';
import { User } from '../_models/User';
import { map, catchError } from 'rxjs/operators';

import { Post } from '../_models/Post';
import { AuthService } from './auth.service';
import { Photo } from '../_models/Photo';

@Injectable({
  providedIn: 'root'
})


export class UserService {

  constructor(private http: HttpClient, private auth: AuthService) {

  }

  baseUrl = 'http://localhost:5050/api'

  isAuthenticated: boolean

  getUser(id: number): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + '/users/' + id);
  }

  getCurrentUserId() {
    return JSON.parse(localStorage.getItem('data')).id
  }

  getCurrentUser() {
    return JSON.parse(localStorage.getItem('data'))
  }

  getUserPosts(id: number): Observable<Post[]> {
    return this.http.get<Post[]>(this.baseUrl + /post/ + id)
  }

  getCurrentUserPohotos() {
    return JSON.parse(localStorage.getItem('data')).photos
  }

  updateUser(id: number, userdata: any) {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.put(this.baseUrl + '/users/' + id, userdata, { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } }).pipe(
      map((result: User) => {
        localStorage.setItem('data', JSON.stringify(result))
        this.auth.changeCurrentUser(result)
        return result;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

  uploadPhoto(data) {
    return this.http.post(this.baseUrl + '/photo', data)
  }

  getAllUsers() {
    return this.http.get(this.baseUrl + '/users')
  }

  getAllUserPhotos(): Observable<Photo> {
    return this.http.get<Photo>(this.baseUrl + '/photo/all')
  }
}



