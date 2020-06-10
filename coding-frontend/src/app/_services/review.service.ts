import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserService } from './user.service';

import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  constructor(private http: HttpClient) { }

  newDraftReview(data: any) {
    return this.http.post(environment.apiUrl + '/review/create', data)
  }

  deleteReview(id: string) {
    return this.http.delete(environment.apiUrl + '/review/' + id + '/delete')
  }

  confirmReview(id: string, data: any) {
    return this.http.put(environment.apiUrl + '/review/' + id + '/update', data)
  }

  getAllReviews(id: string) {
    return this.http.get(environment.apiUrl + '/review/foruser/' + id)
  }

  publishReview(id: string) {
    return this.http.get(environment.apiUrl + '/review/publish/' + id)
  }

  getPublishedReviews(id: string) {
    return this.http.get(environment.apiUrl + '/review/' + id + '/status/published')
  }

}
