import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

import { NbDialogRef } from '@nebular/theme';
import { UserService } from 'src/app/_services/user.service';
import { PostService } from 'src/app/_services/post.service';
import { AlertService } from 'src/app/_services/alert.service';

import { ActivatedRoute, Router } from '@angular/router'

import { NbDialogService } from '@nebular/theme'

import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { Post } from 'src/app/_models/Post';
import { Tag } from 'src/app/_models/Tag';


@Component({
  selector: 'app-edit-post',
  templateUrl: './edit-post.component.html',
  styleUrls: ['./edit-post.component.scss']
})
export class EditPostComponent implements OnInit {

  public editEditor = ClassicEditor;

  editSpinner = false

  @Input() post: any

  tags: Tag[]

  deleteSpinner = false;

  tagId: []

  constructor(
    private alert: AlertService,
    private router: Router,
    private dialogService: NbDialogService,
    private ActivatedRoute: ActivatedRoute,
    private user: UserService,private postService: PostService) { }

  ngOnInit() {
    this.ActivatedRoute.queryParams.subscribe((param) => {
      this.postService.getSinglePost(param['forpost']).subscribe((result) => {
        this.post = result
      })
    })

    this.postService.getAlTags().subscribe((tags) => {
      this.tags = tags
    })
  }

  // ngOnChanges(changes: SimpleChanges): void {
  //   //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
  //   //Add '${implements OnChanges}' to the class.
  //   this.post.postTags = []
  // }

  editItem(id: string, post: any) {
    // post.postTags = this.tagId
    this.editSpinner = true
    console.log(post)
   post.tags = this.post.tags.map(
      (item) => {
        return item.id
      }
    )
    this.postService.editPost(id, post).subscribe(result => {
      this.editSpinner = false
      this.alert.showToast('bottom-left', 'info', 'Update ok', 'Your post has been updated!')
      this.router.navigate(['posts/list'])

    })
  }


   removeTag(id: string) {
    this.postService.deleteTag(id).subscribe(result => {
      this.alert.showToast('top-right', 'info', 'Deleted', 'Tag Deleted')
    })
    const index = this.post.tags.map((item) => {
      return item.id
    }).indexOf(id)
    console.log(index)
    this.post.tags.splice(index, 1);
    
  }

  openAddTagDialog(dialog: NbDialogRef<any>) {
    this.dialogService.open(dialog, {
      context: {
        object: {}
      }, closeOnBackdropClick: false
    }).onClose.subscribe((data: any) => {
      console.log(data)
      this.newTag(data)
      // this.elementAdded = true
      // console.log(this.elementAdded)



    })
  }


  newTag(item: Tag) {
    this.postService.addNewTag(item).subscribe((result) => {
      this.post.tags.push(result)
      this.alert.showToast('bottom-left', 'success', 'Ok', 'Tags for this post updated!')
    }, error => {
      console.log(error)
    })
  }

}
