import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Commentary } from '../../../_models/Comments'
import { PostService } from 'src/app/_services/post.service';
import { AlertService } from 'src/app/_services/alert.service';
import { AlertifyServiceService } from 'src/app/_services/alertify-service.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.sass']
})
export class PostComponent implements OnInit {

  @ViewChild('commentForm', { static: false }) commentForm: NgForm;

  constructor(private postService: PostService, private alert: AlertifyServiceService) { }

  ngOnInit() {
  }

  newComment: Commentary = {
    body: '',
    commenterName: '',
    published: false,
    email: '',
    postId: '08d7f9ab-f802-099c-24b1-7c644bb55552'

  }

  postNewComment() {
    console.log
    this.postService.addComment(this.newComment).subscribe((result) => {
      this.alert.success('Thanks for your comment, you have to wait for the blog owner in order to be published')
      this.commentForm.resetForm(this.newComment)
      this.newComment.postId = '08d7f9ab-f802-099c-24b1-7c644bb55552'

    })
  }



}
