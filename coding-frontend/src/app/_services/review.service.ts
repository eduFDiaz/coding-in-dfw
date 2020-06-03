import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserService } from './user.service';

import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  myheader = {
    headers: {
      'authorization': 'Bearer ' + localStorage.getItem('token'),
      'Content-Type': 'application/json'
    }
  }

  constructor(private http: HttpClient, private user: UserService) { }

  newReview() {
    return this.http.post(environment.apiUrl + '/review/create',
     this.myheader  )
  }

  deleteReview(id: string) {
    return this.http.delete(environment.apiUrl + '/review/' + id + '/delete')
  }



}
