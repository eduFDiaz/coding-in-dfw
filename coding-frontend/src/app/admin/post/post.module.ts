import { NgModule } from '@angular/core';

import { CommonModule } from '@angular/common';
import { PostlistComponent } from './postlist/postlist.component';
import { AddpostComponent } from './addpost/addpost.component';

import { JwPaginationComponent } from 'jw-angular-pagination';

import { FormsModule } from '@angular/forms'

// import { CKEditorModule } from 'ng2-ckeditor';

import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

import { NbAlertModule, NbSelectModule, NbBadgeModule, NbIconModule, NbTreeGridModule, NbCardModule, NbInputModule, NbButtonModule, NbSpinnerModule } from '@nebular/theme';
import { EditPostComponent } from './postlist/edit-post/edit-post.component';
import { DeletePostComponent } from './postlist/delete-post/delete-post.component';
import { AddtagComponent } from './addpost/addtag/addtag.component';
import { TaglistComponent } from './taglist/taglist.component'
import { CommentsComponent } from './comments/comments.component';
import { AlertService } from 'src/app/_services/alert.service';
import { PostListComponent } from 'src/app/pages/blog/post-list/post-list.component';

@NgModule({
  providers: [AlertService],
  declarations: [PostlistComponent,
    AddpostComponent,
    EditPostComponent,
    DeletePostComponent,
    AddtagComponent,
    TaglistComponent,
    JwPaginationComponent,
    CommentsComponent
  ],
  imports: [
    CommonModule,
    NbBadgeModule,
    NbTreeGridModule,
    NbIconModule,
    NbCardModule,
    CKEditorModule,
    FormsModule,
    NbInputModule,
    NbButtonModule,
    NbSpinnerModule,
    NbSelectModule,
    NbAlertModule,
  ],
  exports: [PostlistComponent],
  entryComponents: [EditPostComponent, DeletePostComponent, AddtagComponent, TaglistComponent]
})
export class PostModule { }
