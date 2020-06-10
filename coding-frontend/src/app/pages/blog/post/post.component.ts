import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Commentary } from '../../../_models/Comments'
import { PostService } from 'src/app/_services/post.service';

import { AlertifyServiceService } from 'src/app/_services/alertify-service.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/_models/Post';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.sass']
})
export class PostComponent implements OnInit {

  @ViewChild('commentForm', { static: false }) commentForm;

  constructor(private postService: PostService, private alert: AlertifyServiceService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.subscribe((data) => {
      this.postService.getSinglePost(data.id).subscribe((result: Post) => {
        this.post = result
        console.log(this.post)
      })
    })
  }

  post: Post

  newComment: Commentary = {
    body: '',
    commenterName: '',
    published: false,
    email: '',
    postId: ''

  }

  postNewComment() {
    console.log(this.post.id)
    this.newComment.postId = this.post.id
    console.log(this.newComment)
    this.postService.addComment(this.newComment).subscribe((result) => {
      this.alert.success('Thanks for your comment, you have to wait for the blog owner in order to be published')
      this.commentForm.resetForm({})
    })
  }



}
