import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AlertService } from './alert.service';
import { UserService } from './user.service';
import { Post } from '../_models/Post';
import { Tag } from '../_models/Tag';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private currentPosts = new BehaviorSubject<Post[]>([])
  private currentTags = new BehaviorSubject<Tag[]>([])

  updatedPosts = this.currentPosts.asObservable();
  updatedTags = this.currentTags.asObservable();

  httpOptions = {
    headers: new HttpHeaders({
      Autorization: 'Bearer ' + localStorage.getItem('token')
    })
  };

  baseUrl = 'http://localhost:5050/api'

  constructor(private route: Router, private http: HttpClient, private toast: AlertService, private user: UserService) { }

  newPost(postdata) {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.post(this.baseUrl + '/post/create', postdata, { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } }).pipe(
      map((result: any) => {
        if (result) {
          this.toast.showToast('bottom-left', 'success', 'Post uploaded!', 'Your post was uploaded succesfully')
          this.route.navigate(['/posts/list'])
          this.currentPosts.next(result)
        }
        return result;
      }), catchError(error => {
        console.log(error)
        this.toast.showToast('top-right', 'danger', 'Error `$error.statusText`', 'There was a problem trying to upload the post')
        // return throwError('Something went wrong!');
        return throwError('Something went wrong!');

      })
    )
  }

  getUserPosts(userid: number): Observable<Post[]> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.get<Post[]>(this.baseUrl + '/post/foruser/' + userid, { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } }).pipe(
      map((result) => {
        return result
      }, catchError(err => {
        return err
      }))
    )
  }

  deletePost(postid: number): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.delete<boolean>(this.baseUrl + '/post/' + postid + '/delete', { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } }).pipe(
      map((result: boolean) => {
        // tslint:disable-next-line: no-shadowed-variable
        this.getUserPosts(this.user.getCurrentUserId()).subscribe((result) => {
          this.currentPosts.next(result)
        })
        return result
      }, catchError(error => {
        return throwError(error)
      }))
    )
  }

  editPost(postid: number, newdata: any): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.put<boolean>(this.baseUrl + '/post/' + postid + '/update', newdata, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    })
      .pipe(
        map((response: boolean) => {
          this.getUserPosts(this.user.getCurrentUserId()).subscribe((result) => {
            this.currentPosts.next(result)
          })
          return response
        }, catchError(err => {
          return err
        }))
      )
  }

  // If using the newer Backend please use /tag/all
  getAlTags(): Observable<Tag[]> {
    return this.http.get<Tag[]>(this.baseUrl + '/tag/all', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map((result) => {
        return result
      }, catchError(err => {
        return throwError("There is no tags!")
      }))
    )
  }

  addNewTag(newtag: Tag): Observable<Tag> {
    return this.http.post<Tag>(this.baseUrl + '/tag/create', newtag, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map((result) => {
        this.getAlTags().subscribe((result) => {
          this.currentTags.next(result)
        })
        return result
      }, catchError(error => {
        return error
      }))
    )
  }

  deleteTag(tagid: number) {
    return this.http.delete(this.baseUrl + '/tag/' + tagid + '/delete', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map((result) => {
        this.getAlTags().subscribe((tags) => {
          this.currentTags.next(tags)
        })
        return result
      }, catchError(error => {
        return error
      }))
    )
  }

  editTag(tagid: number, newdata: any): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.put<boolean>(this.baseUrl + '/tag/' + tagid + '/update', newdata, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    })
      .pipe(
        map((response: boolean) => {
          this.getAlTags().subscribe((result) => {
            this.currentTags.next(result)
          })
          return response
        }, catchError(err => {
          return err
        }))
      )
  }

}
