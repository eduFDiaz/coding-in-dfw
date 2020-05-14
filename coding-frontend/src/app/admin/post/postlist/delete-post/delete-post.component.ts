import { Component, OnInit, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { UserService } from 'src/app/_services/user.service';
import { PostService } from 'src/app/_services/post.service';
import { AlertService } from 'src/app/_services/alert.service';



@Component({
  selector: 'app-delete-post',
  templateUrl: './delete-post.component.html',
  styleUrls: ['./delete-post.component.scss']
})
export class DeletePostComponent implements OnInit {

  @Input() post: any

  deleteSpinner = false;

  constructor(private user: UserService, protected dialogRef: NbDialogRef<any>, private postService: PostService, private toast: AlertService) { }

  ngOnInit() {

  }

  close(result: any) {
    this.dialogRef.close(result);
  }

  deletePost(postid: number) {
    this.deleteSpinner = true
    this.postService.deletePost(postid).subscribe(result => {
      this.deleteSpinner = false
      this.toast.showToast('bottom-left', 'info', 'Delete ok', 'Your post has been deleted!')
      this.postService.getUserPosts(this.user.getCurrentUserId()).subscribe((data) => {
        let postslenght
        postslenght = data.length
        this.dialogRef.close(postslenght)
      }, err => {
        const myerr = err
        this.dialogRef.close(myerr)
      })

    })

  }
}
