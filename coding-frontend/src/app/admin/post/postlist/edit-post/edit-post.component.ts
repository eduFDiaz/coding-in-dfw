import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

import { NbDialogRef } from '@nebular/theme';
import { UserService } from 'src/app/_services/user.service';
import { PostService } from 'src/app/_services/post.service';
import { AlertService } from 'src/app/_services/alert.service';




import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Post } from 'src/app/_models/Post';
import { Tag } from 'src/app/_models/Tag';


@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.scss']
})
export class EditPostComponent implements OnInit, OnChanges {

  public editEditor = ClassicEditor;

  editSpinner = false

  @Input() post: any

  tags: Tag[]

  deleteSpinner = false;

  newTagIds: []

  constructor(private user: UserService, protected dialogRef: NbDialogRef<any>, private postService: PostService, private toast: AlertService) { }

  ngOnInit() {
    this.postService.getAlTags().subscribe((tags) => {
      this.tags = tags
    })

  }

  ngOnChanges(changes: SimpleChanges): void {
    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.
    this.post.postTags = []
  }

  editItem(id: string, post: any) {
    post.postTags = this.newTagIds
    this.editSpinner = true
    console.log(post)
    this.postService.editPost(id, post).subscribe(result => {
      this.editSpinner = false
      this.toast.showToast('bottom-left', 'info', 'Update ok', 'Your post has been updated!')
      this.postService.getUserPosts(this.user.getCurrentUserId()).subscribe((data) => {
        let postslenght
        postslenght = data.length
        this.dialogRef.close(postslenght)
      }, err => {
        const myerr = err
        this.dialogRef.close(myerr)
      })
      this.dialogRef.close()
    })

  }

}
