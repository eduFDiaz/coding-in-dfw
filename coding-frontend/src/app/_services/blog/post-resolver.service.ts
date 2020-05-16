import { Injectable } from '@angular/core';
import { PostService } from '../post.service';
import { Router, Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Post } from 'src/app/_models/Post';
import { take, mergeMap } from 'rxjs/operators';
import { of, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostResolverService {

  constructor(private postService: PostService, private router: Router) { }

  // resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Post> | Observable<never> {
  //   let id = route.paramMap.get('id');

  // return this.postService.getSinglePost(id).pipe(
  //   take(1),
  //   mergeMap(crisis => {
  //     if (crisis) {
  //       return of(crisis);
  //     } else { // id not found
  //       this.router.navigate(['/crisis-center']);
  //       return EMPTY;
  //     }
  //   })
  // );

}
}
