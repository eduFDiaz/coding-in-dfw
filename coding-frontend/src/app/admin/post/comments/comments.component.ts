import { Component, OnInit, TemplateRef } from '@angular/core';
import { PostService } from 'src/app/_services/post.service';
import { Commentary } from 'src/app/_models/Comments';
import { NbDialogService } from '@nebular/theme';
import { AlertService } from 'src/app/_services/alert.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.css']
})
export class CommentsComponent implements OnInit {

  commentSpinner = false;

  pageOfItems: Array<any>;

  unpublishedComments: Commentary[]

  constructor(private postService: PostService, private dialogService: NbDialogService, private alert: AlertService) { }

  ngOnInit() {
    this.commentSpinner = true
    this.postService.getUnpublishedComments().subscribe((comments) => {
      this.unpublishedComments = comments
      console.log(this.unpublishedComments)
      this.commentSpinner = false
    })
  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
    this.pageOfItems = pageOfItems;
  }

  openDeleteDialog(dialog: TemplateRef<any>, data: any) {
    this.dialogService.open(dialog, {
      context: {
        object: data
      }
    }).onClose.subscribe((result) => {
      if (result) {
        this.commentSpinner = true
        this.postService.deleteComment(data.id).subscribe(() => {
          this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Bye bye you!')
          this.unpublishedComments = this.unpublishedComments.filter((obj: any) => obj.id !== data.id)
          this.commentSpinner = false
        })
      }
    })
  }

  aproveComment(id: string) {
    this.commentSpinner = true
    this.postService.publishComment(id).subscribe(() => {
      this.alert.showToast('bottom-left', 'success', 'Publish Ok', 'The comment its visible now!')
      this.unpublishedComments = this.unpublishedComments.filter((obj: any) => obj.id !== id)
      this.commentSpinner = false
    })
  }

}