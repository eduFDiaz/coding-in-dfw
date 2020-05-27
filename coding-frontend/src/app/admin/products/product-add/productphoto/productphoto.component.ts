import { Component, OnInit, Input } from '@angular/core';

import { FileUploader } from "ng2-file-upload";
import { ProductPhoto } from 'src/app/_models/ProductPhoto';
import { environment } from 'src/environments/environment';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'src/app/_services/alert.service';

@Component({
  selector: 'app-productphoto',
  templateUrl: './productphoto.component.html',
  styleUrls: ['./productphoto.component.sass']
})
export class ProductphotoComponent implements OnInit {

  @Input() photos: ProductPhoto[];

  forproduct: string

  uploader: FileUploader;

  currentMainPhoto: ProductPhoto;

  hasBaseDropZoneOver: boolean;

  constructor(private route: ActivatedRoute, private alert: AlertService, private router: Router) { }

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.forproduct = params['forproduct']
    })
    console.log(this.forproduct)
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
      url: environment.apiUrl + '/photo/product/' + this.forproduct + '/create',
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
      console.log("asdasdsadasd")
      console.log(status)
      if (response) {
        const res: ProductPhoto = JSON.parse(response);
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
    this.router.navigate(['product/list'])
  }


}
