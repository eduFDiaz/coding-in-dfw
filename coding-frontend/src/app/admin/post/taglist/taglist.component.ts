import { Component, OnInit, TemplateRef } from '@angular/core';


import { NbDialogService } from '@nebular/theme'
import { Tag } from 'src/app/_models/Tag';
import { UserService } from 'src/app/_services/user.service';
import { AlertService } from 'src/app/_services/alert.service';
import { PostService } from 'src/app/_services/post.service';



@Component({
  selector: 'app-taglist',
  templateUrl: './taglist.component.html',
  styleUrls: ['./taglist.component.scss']
})
export class TaglistComponent implements OnInit {

  spinner = false

  pageOfItems: Array<any>;

  tags: Tag[]

  newTag: Tag = {
    title: '',
    description: '',

  }

  constructor(private user: UserService, private alert: AlertService, private postService: PostService, private dialogService: NbDialogService) { }

  ngOnInit() {
    this.spinner = false
    this.postService.getAlTags().subscribe((result) => {
      console.log(result)
      this.tags = result
      this.spinner = false
    })

  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
    this.pageOfItems = pageOfItems;
  }

  openDeleteDialog(dialog: TemplateRef<any>, tag: any) {
    this.dialogService.open(dialog, {
      context: 'Do you want to delete this tag?'
    }).onClose.subscribe(
      (result) => {
        this.spinner = true
        if (result === 'delete') {
          console.log(result)
          this.postService.deleteTag(tag.id).subscribe((ok) => {
            this.alert.showToast('bottom-left', 'info', 'Tag deleted!', 'Your tag was deleted!')
            this.spinner = false
          })
          this.postService.updatedTags.subscribe((freshtags) => {
            this.tags = freshtags

          })
        }
        this.spinner = false
      }
    )
  }

  test(data) {
    console.log(data)
  }

  openEditDialog(dialog: TemplateRef<any>, mycontext: any) {
    // this.spinner = true
    this.dialogService.open(dialog, {
      context: mycontext
    }).onClose.subscribe(
      (result) => {
        if (result) {
          console.log(result)
          const tagid = result.id
          const tagbody = {
            title: result.title,
            description: result.description
          }
          this.alert.showToast('top-right', 'success', 'Update', 'Your tag was updated')
          // this.spinner = false
          this.postService.editTag(tagid, tagbody).subscribe((ok) => {
            console.log(ok)
          })
          this.postService.updatedTags.subscribe((freshtags) => {
            this.tags = freshtags
            this.spinner = false
          }
          )

        }

      }
    )
  }

  openAddDialog(dialog: TemplateRef<any>, data: any) {
    this.dialogService.open(dialog, {
      context: data
    }).onClose.subscribe(
      (result) => {
        this.spinner = true
        this.postService.addNewTag(result).subscribe((data) => {
          this.alert.showToast('top-right', 'success', 'Created', 'Your tag was created!')
          this.postService.updatedTags.subscribe((result) => {
            this.tags = result
            this.newTag = {
              title: '',
              description: '',

            }
          })
          this.spinner = false


        })

      }
    )


  }
}
