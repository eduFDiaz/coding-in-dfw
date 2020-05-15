import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/_services/post.service';
import { AuthService } from 'src/app/_services/auth.service';
import { User } from 'src/app/_models/User';
import { Post } from 'src/app/_models/Post';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.sass']
})
export class PostListComponent implements OnInit {

  currentUser: User
  posts: Post[]

  constructor(private postService: PostService, private auth: AuthService) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('data'))
    this.postService.getUserPosts(this.currentUser.id).subscribe((posts: Post[]) => {
      this.posts = posts
    })
  }

}
