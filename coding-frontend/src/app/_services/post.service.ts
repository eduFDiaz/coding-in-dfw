import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AlertService } from './alert.service';
import { UserService } from './user.service';
import { Post } from '../_models/Post';
import { Tag } from '../_models/Tag';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Commentary } from '../_models/Comments';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  private currentPosts = new BehaviorSubject<Post[]>([])
  private currentTags = new BehaviorSubject<Tag[]>([])

  updatedPosts = this.currentPosts.asObservable();
  updatedTags = this.currentTags.asObservable();

  constructor(private http: HttpClient, private toast: AlertService, private user: UserService) { }

  newPost(postdata): Observable<Post> {
    return this.http.post(environment.apiUrl + '/post/create', postdata).pipe(
      map((result: any) => {
        if (result) {
          this.currentPosts.next(result)
        }
        return result;
      }), catchError(error => {

        // this.toast.showToast('top-right', 'danger', 'Error `$error.statusText`', 'There was a problem trying to upload the post')
        // return throwError('Something went wrong!');
        // return throwError('Something went wrong!');
        return error

      })
    )
  }

  getSinglePost(postId: string): Observable<Post> {
    return this.http.get<Post>(environment.apiUrl + '/post/' + postId)
  }

  getUnpublishedComments(): Observable<Commentary[]> {
    return this.http.get<Commentary[]>(environment.apiUrl + '/comment/unpublished')
  }

  publishComment(id: string) {
    return this.http.put(environment.apiUrl + '/comment/' + id + '/publish', {})
  }

  deleteComment(id: string) {
    return this.http.delete(environment.apiUrl + '/comment/' + id + '/delete')
  }

  getUserPosts(userid: string): Observable<Post[]> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.get<Post[]>(environment.apiUrl + '/post/foruser/' + userid).pipe(
      map((result) => {
        return result
      }, catchError(err => {
        return err
      }))
    )
  }

  deletePost(postid: number): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.delete<boolean>(environment.apiUrl + '/post/' + postid + '/delete').pipe(
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

  editPost(postid: string, newdata: any): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.put<boolean>(environment.apiUrl + '/post/' + postid + '/update', newdata)
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
    return this.http.get<Tag[]>(environment.apiUrl + '/tag/all').pipe(
      map((result) => {
        return result
      }, catchError(err => {
        return throwError("There is no tags!")
      }))
    )
  }

  addNewTag(newtag: Tag): Observable<Tag> {
    return this.http.post<Tag>(environment.apiUrl + '/tag/create', newtag).pipe(
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

  deleteTag(tagid: string) {
    return this.http.delete(environment.apiUrl + '/tag/' + tagid + '/delete').pipe(
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

  editTag(tagid: string, newdata: any): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.put<boolean>(environment.apiUrl + '/tag/' + tagid + '/update', newdata)
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

  addComment(comment: Commentary) {
    return this.http.post<Commentary>(environment.apiUrl + '/comment/create', comment)
  }

}
