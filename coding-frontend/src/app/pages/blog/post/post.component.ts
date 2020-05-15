import { Component, OnInit } from '@angular/core';
import { Commentary } from '../../../_models/Comments'

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.sass']
})
export class PostComponent implements OnInit {



  constructor() { }

  ngOnInit() {
  }

  newComment: Commentary = {
    body: '',
    commenterName: '',
    published: false,
    email: ''

  }



}
