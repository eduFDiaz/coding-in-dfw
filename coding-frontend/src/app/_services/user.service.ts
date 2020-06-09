import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


import { Observable, throwError } from 'rxjs';
import { User } from '../_models/User';
import { map, catchError } from 'rxjs/operators';

import { Post } from '../_models/Post';
import { AuthService } from './auth.service';
import { Photo } from '../_models/Photo';
import { environment } from 'src/environments/environment'
@Injectable({
  providedIn: 'root'
})

export class UserService {

  constructor(private http: HttpClient, private auth: AuthService) {

  }

  myheader = {
    headers: {
      'authorization': 'Bearer ' + localStorage.getItem('token'),
      'Content-Type': 'application/json'
    }
  }


  isAuthenticated: boolean

  getUser(id: string): Observable<User> {
    return this.http.get<User>(environment.apiUrl + '/users/' + id);
  }

  getCurrentUserId() {
    return JSON.parse(localStorage.getItem('data')).id
  }

  getCurrentUser() {
    return JSON.parse(localStorage.getItem('data'))
  }

  getUserPosts(id: string): Observable<Post[]> {
    return this.http.get<Post[]>(environment.apiUrl + /post/ + id, this.myheader)
  }

  getCurrentUserPohotos(): Observable<Photo[]> {
    return this.http.get<Photo[]>(environment.apiUrl + '/photo/all')
  }

  updateUser(id: string, userdata: any) {
    return this.http.put(environment.apiUrl + '/users/' + id, userdata).pipe(
      map((result: User) => {
        localStorage.setItem('data', JSON.stringify(result))
        // this.auth.changeCurrentUser(result)
        return result;
      }), catchError(error => {
        return throwError('Something went wrong!');
      })
    )
  }

  uploadPhoto(data) {
    return this.http.post(environment.apiUrl + '/photo', data, this.myheader)
  }

  DeletePhoto(photoId: string) {
    return this.http.delete(environment.apiUrl + '/photo/' + photoId, this.myheader);
  }

  getAllUsers() {
    return this.http.get(environment.apiUrl + '/users')
  }

  getAllUserPhotos(): Observable<Photo> {
    return this.http.get<Photo>(environment.apiUrl + '/photo/all')
  }

  setAsMainPhoto(item: string, photoId: string) {
    return this.http.post(environment.apiUrl + '/photo/' + item + '/' + photoId + '/setMain', {}, this.myheader)
  }
}
