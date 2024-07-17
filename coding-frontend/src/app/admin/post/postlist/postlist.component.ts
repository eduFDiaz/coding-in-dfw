import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { NbDialogService } from '@nebular/theme'



import { DatePipe } from '@angular/common'

import { DeletePostComponent } from './delete-post/delete-post.component';
import { Post } from 'src/app/_models/Post';
import { AlertService } from 'src/app/_services/alert.service';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { PostService } from 'src/app/_services/post.service';


@Component({
  selector: 'app-postlist',
  templateUrl: './postlist.component.html',
  styleUrls: ['./postlist.component.scss']
})


export class PostlistComponent implements OnInit {

  succes: boolean

  pageOfItems: Array<any>;

  spinner = false
  data: any
  userposts: Post[]

  constructor(private activeRoute: ActivatedRoute, private route: Router, private toast: AlertService, private dialog: NbDialogService, private user: UserService, private auth: AuthService, private postService: PostService) { }

  ngOnInit() {
    this.activeRoute.queryParams.subscribe((params) => {
      if (params['success']) {
        this.toast.showToast('top-right', 'success', 'Created!', 'Your post was created succesfully')
      }
    })
    this.spinner = true
    this.postService.getUserPosts(this.user.getCurrentUserId()).subscribe((result) => {
      this.userposts = result
      this.spinner = false

    }
    )
  }

  openDeleteDialog(postToDelete: Object) {
    this.dialog.open(DeletePostComponent, {
      context: {
        post: postToDelete
      }, closeOnBackdropClick: false
    }).onClose.subscribe((data) => {

      if (data) {
        if (data == 0) {
          this.spinner = false
          this.userposts = []
          this.toast.showToast('top-right', 'info', 'Theres no posts here :(', 'Cant find any product')
        } else {
          this.postService.updatedPosts.subscribe((result) => {
            if (result) {
              this.userposts = result
              this.spinner = false;
            }
          })
        }
        if (data === 'closed') {
          this.postService.getUserPosts(this.user.getCurrentUserId()).subscribe((result) => {
            this.userposts = result
            this.spinner = false
          }
          )
        }
      }
    });
  }

  openEditDialog(id: string) {
    this.route.navigate(['posts/edit'], {
      queryParams: {
        forpost: id
      }
    })
    // this.dialog.open(EditPostComponent, {
    //   context: {
    //     post: postToEdit
    //   }, closeOnBackdropClick: true
    // })
    // .onClose.subscribe((data) => {
    // });
  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
    this.pageOfItems = pageOfItems;
  }

  goToAddPost() {
    this.route.navigate(['admin/posts/new'])
  }

}
