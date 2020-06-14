import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(environment.apiUrl + 'newsletter/all')
  }

  unsubscribe(id: string) {
    return this.http.delete(environment.apiUrl + '/newsletter/' + id + '/delete')
  }

}
