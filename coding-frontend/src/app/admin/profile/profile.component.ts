import { Component, OnInit, ViewChild, HostListener, TemplateRef, AfterViewInit } from '@angular/core';
import { NbDialogService } from '@nebular/theme';

import { UserService } from '../../_services/user.service'
import { AuthService } from '../../_services/auth.service'
import { AlertService } from '../../_services/alert.service'
import { NgForm } from '@angular/forms';

import { PhotoaddComponent } from './photoadd/photoadd.component'
import { User } from '../../_models/User';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  entryComponents: [PhotoaddComponent]
})



export class ProfileComponent implements OnInit {
  updateSpinner = false
  profileSpinner = false;
  userid: number
  myuser: any
  @ViewChild('dialogRef', { static: true }) dialogRef: TemplateRef<any>;

  @ViewChild('editProfileForm', { static: false }) editProfileForm: NgForm;


  // @HostListener("window:beforeunload", ["$event"])
  // unloadNotification($event: any) {
  //   if (this.editProfileForm.dirty) {
  //     $event.returnValue = true;
  //   }
  // }



  constructor(private user: UserService, private auth: AuthService, private alert: AlertService, private dialogService: NbDialogService) { }

  // open(dialog: TemplateRef<any>) {
  //   this.dialogService.open(dialog, { context: 'this is some additional data passed to dialog' });
  // }

  open() {
    this.dialogService.open(PhotoaddComponent, { context: 'this is some additional data passed to dialog' });
  }

  openAddTagDialog() {
    this.dialogService.open(PhotoaddComponent, { closeOnBackdropClick: false }).onClose.subscribe(
      (data) => {
        console.log(data)
      }
    )
  }


  ngOnInit() {
    this.profileSpinner = true

    this.user.getUser(this.user.getCurrentUserId()).subscribe((result) => {
      this.myuser = result
      this.profileSpinner = false
    })
  }

  updateProfile(userdata: any) {
    this.updateSpinner = true;
    this.user.updateUser(this.user.getCurrentUserId(), userdata).subscribe(
      (value: User) => {
        this.updateSpinner = false;
        this.myuser = value
        // this.auth.changeCurrentUser(value);
        // localStorage.setItem('data', JSON.stringify(this.myuser))
        this.alert.showToast('bottom-left', 'success', 'Profile Update!', 'Profile updated succefuly')
        this.editProfileForm.reset(this.myuser);
      },
      error => {
        this.updateSpinner = false;
        this.alert.showToast('top-right', 'danger', error, 'There was an error!')
      }
    );
  }



}
