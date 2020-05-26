import { Component, OnInit, ViewChild } from '@angular/core';
// import { CKEditorModule } from 'ng2-ckeditor';

import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';

import { AddtagComponent } from './addtag/addtag.component'

import * as moment from 'moment';

import { NbDialogService } from '@nebular/theme'

import './ckeditor-loader'
import { NgForm } from '@angular/forms';

import { Router } from '@angular/router';
import { Tag } from 'src/app/_models/Tag';
import { UserService } from 'src/app/_services/user.service';
import { PostService } from 'src/app/_services/post.service';
import { AlertService } from 'src/app/_services/alert.service';
import { Post } from 'src/app/_models/Post';
// import 'ckeditor';

@Component({
  selector: 'app-addpost',
  templateUrl: './addpost.component.html',
  styleUrls: ['./addpost.component.scss']
})
export class AddpostComponent implements OnInit {


  public Editor = ClassicEditor;

  now = moment().format('LLLL');

  tags: Tag[]

  postSpinner = false

  @ViewChild('newPostForm', { static: false }) newPostForm: NgForm;
  @ViewChild('mytext', { static: false }) mytext: any;

  public postBody = {
    title: '',
    description: '',
    text: '<p>Hello, world!</p>',
    userid: 0,
    readingTime: 0,
    postTagId: ''
  };



  constructor(private dialog: NbDialogService, private route: Router, private user: UserService, private toPost: PostService, private toast: AlertService) { }

  ngOnInit() {
    this.toPost.getAlTags().subscribe((result) => {
      this.tags = result
      console.log(result)
    })
  }

  recibeNewTag($event) {
    console.log($event)
    this.tags.push($event)
  }

  getReadingTime(postbody: string) {
    const wordCount = postbody.replace(/[^\w ]/g, "").split(/\s+/).length;

    const readingTimeInMinutes = Math.floor(wordCount / 228) + 1;
    const readingTimeAsString = readingTimeInMinutes + " min";

    return readingTimeInMinutes
  }

  postNow(data: any) {
    data.publishedAt = this.now
    data.readingTime = this.getReadingTime(data.text)
    data.userid = this.user.getCurrentUserId()
    this.postSpinner = true
    this.toPost.newPost(data).subscribe((request) => {
      this.postSpinner = false
      this.toast.showToast('bottom-left', 'success', 'Post uploaded!', 'Your post was uploaded succesfully')
      this.route.navigate(['admin/posts/new/photo'], { queryParams: { forpost: request.id } })

    }, error => {
      this.postSpinner = false;
    })
  }

  openAddTagDialog() {
    this.dialog.open(AddtagComponent, { closeOnBackdropClick: true }).onClose.subscribe(
      (data) => {
        if (data) {
          this.tags.push(data)
        }
      }
    )
  }

}
