import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Faq } from '../_models/Faq';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FaqServiceService {

  constructor(private http: HttpClient) { }

  getFaqs(userid?: string): Observable<Faq> {
    return this.http.get<Faq>(environment.apiUrl + '/faq/all')
  }

  newFaq(data: Faq) {
    return this.http.post(environment.apiUrl + '/faq/create', data)
  }

  editFaq(id: string, data: Faq) {
    return this.http.put(environment.apiUrl + '/faq/' + id + '/update', data)
  }

  deleteFaq(id: string) {
    return this.http.delete(environment.apiUrl + '/faq/' + id + '/delete')
  }

  getPricing(userid: string) {
    return this.http.get(environment.apiUrl + '/service/foruser' + userid)
  }

}
