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
  postResume: string

  constructor(private postService: PostService, private auth: AuthService) { }

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('userdata'))
    this.postService.getUserPosts(this.currentUser.id).subscribe((posts: Post[]) => {
      this.posts = posts
    })

  }

  stripHtml(html: string) {
    var div = document.createElement("DIV");

    div.innerHTML = html;

    let cleanText = div.innerText;

    div = null; // prevent mem leaks

    return cleanText;
  }

}
