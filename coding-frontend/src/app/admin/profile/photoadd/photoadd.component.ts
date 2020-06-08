import { Component, OnInit, Input, EventEmitter, ViewChild, ElementRef } from '@angular/core';

// import { FileUploadModule } from "ng2-file-upload";

import { FileUploader } from "ng2-file-upload";

// import { FileUploader } from 'ng2-file-upload/ng2-file-upload';



import { UserService } from '../../../_services/user.service'
import { AuthService } from '../../../_services/auth.service'
import { Photo } from '../../../_models/Photo';
import { map, catchError } from 'rxjs/operators';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photoadd',
  templateUrl: './photoadd.component.html',
  styleUrls: ['./photoadd.component.scss']
})


export class PhotoaddComponent implements OnInit {


  @Input() photos: Photo[];

  uploader: FileUploader;
  // hasBaseDropZoneOver = false;
  baseUrl = 'http://localhost:5050/api'

  currentMainPhoto: Photo;

  userdata: any



  hasBaseDropZoneOver: boolean;

  constructor(private user: UserService, private auth: AuthService) { }

  ngOnInit() {
    this.initializeUploader();

    this.user.getAllUserPhotos().subscribe((photos: any) => {
      this.photos = photos
      console.log(this.photos)
    })
    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;
    }
  }

  fileOverBase(e) {
    this.hasBaseDropZoneOver = e;
  }


  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + '/Photo/' + this.user.getCurrentUserId() + '/create',
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
      console.log(response)
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
        if (photo.isMain) {
          this.auth.changeMemberPhoto(photo.url);
          // this.auth.loggedInUser.photoUrl = photo.url;
        }
      }
    };
  }

  setAsMainPhoto(photo: Photo) {
    this.user.setAsMainPhoto(this.photos[0].userId,photo.id)
      .subscribe(() => {
        this.currentMainPhoto = this.photos.filter(p => p.isMain === true)[0];
        this.currentMainPhoto.isMain = false;
        photo.isMain = true;
        this.auth.changeMemberPhoto(photo.url);
      }, error => { console.log(error); }
        , () => { });
  }

  DeletePhoto(photo: Photo) {
    // this.alertify.confirm('Are you sure you want to delete the photo with id: ' + photo.id.toString() + '?',
    //   () => {
    //     return this.deletePicture(photo);
    //   });
    return this.deletePicture(photo)
  }

  deletePicture(photo: Photo) {
    this.user.DeletePhoto(photo.id)
      .subscribe(() => {
        const id = this.photos.indexOf(photo);
        this.photos = this.photos.filter((data, idx) => idx !== id);
        // this.alertify.success('Photo deleted successfully!');

      }, error => { console.log("Error no se pudo borrar la foto") }
        , () => { });
  }




}
