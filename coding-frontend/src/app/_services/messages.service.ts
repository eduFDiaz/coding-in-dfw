import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AlertService } from './alert.service';
import { UserService } from './user.service';
import { Message } from '../_models/Message';

import { environment } from 'src/environments/environment';
import { Commentary } from '../_models/Comments';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

   constructor(
     private http: HttpClient,
     private toast: AlertService,
     private user: UserService) { }

   createMessage(data: Message) {
   	return this.http.post(environment.apiUrl + '/message/create',data,
   	 { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } } )
   }

   getMessages(): Observable<Message[]> {
   	return this.http.get<Message[]>(environment.apiUrl + '/message/all')
   }

   deleteMessage(id: string) {
   	return this.http.delete(environment.apiUrl + '/message/')
   }


}
