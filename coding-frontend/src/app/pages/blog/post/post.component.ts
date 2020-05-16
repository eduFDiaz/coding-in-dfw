import { Component, OnInit } from '@angular/core';
import { Commentary } from '../../../_models/Comments'
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.sass']
})
export class PostComponent implements OnInit {



  constructor(private postService: PostService) { }

  ngOnInit() {
  }

  newComment: Commentary = {
    body: '',
    commenterName: '',
    published: false,
    email: '',
    postId: '08d7f90b-a335-f3fe-7010-b8c753395ab3'

  }

  postNewComment() {
    console.log
    this.postService.addComment(this.newComment).subscribe((result) => {
      console.log(result)

    })
  }



}
