import { Component, OnInit, Input } from '@angular/core';

import { FileUploader } from "ng2-file-upload";

import { environment } from 'src/environments/environment';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'src/app/_services/alert.service';
import { Route } from '@angular/compiler/src/core';
import { PostPhoto } from 'src/app/_models/PostPhoto';

@Component({
  selector: 'app-addphotopost',
  templateUrl: './addphotopost.component.html',
  styleUrls: ['./addphotopost.component.sass']
})
export class AddphotopostComponent implements OnInit {

  @Input() photos: PostPhoto[];

  forpost: string

  uploader: FileUploader;

  currentMainPhoto: PostPhoto;

  hasBaseDropZoneOver: boolean;

  constructor(private alert: AlertService, private router: Router, private activatedRouter: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRouter.queryParams.subscribe((params) => {
      this.forpost = params['forpost']
    })
    console.log(this.forpost)
    this.initializeUploader()
    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;
    }
    this.photos = []

  }

  fileOverBase(e) {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: environment.apiUrl + '/photo/post/' + this.forpost + '/create',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 15 * 1024 * 1024,

    });
    // This fixes a CORS bug
    this.uploader.onAfterAddingFile = (file) => { file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      // console.log(response)
      console.log(status)
      if (response) {
        const res: PostPhoto = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
        if (photo.isMain) {
          // this.auth.changeMemberPhoto(photo.url);
          // this.auth.loggedInUser.photoUrl = photo.url;
        }
        this.success()
      }
    };
  }

  success() {
    this.router.navigate(['admin/posts/list'], {
      queryParams: {
        success: true
      }
    })
  }

}
