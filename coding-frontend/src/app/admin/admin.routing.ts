import { Routes, RouterModule } from '@angular/router'
import { AdminComponent } from './admin.component';
import { LoginComponent } from './login/login.component';
import { ResumeAdminComponent } from './resume-admin/resume-admin.component';
import { ProfileComponent } from './profile/profile.component';
import { PostlistComponent } from './post/postlist/postlist.component';
import { AuthGuardService } from '../_services/auth-guard.service';
import { AddpostComponent } from './post/addpost/addpost.component';
import { ProductListComponent } from './products/product-list/product-list.component';
import { TaglistComponent } from './post/taglist/taglist.component';
import { ProductAddComponent } from './products/product-add/product-add.component';
import { PhotoaddComponent } from './profile/photoadd/photoadd.component';
import { CommentsComponent } from './post/comments/comments.component';
import { FaqsComponent } from './faqs/faqs.component';
import { ProductphotoComponent } from './products/product-add/productphoto/productphoto.component';
import { AddphotopostComponent } from './post/addpost/addphotopost/addphotopost.component';
import { ProductEditComponent } from './products/product-list/product-edit/product-edit.component';
import { EditPostComponent } from './post/postlist/edit-post/edit-post.component'
import { MessagesComponent } from './messages/messages.component'
import { ReviewsComponent } from './reviews/reviews.component'
import { FeaturedskillsComponent } from './resume-admin/featuredskills/featuredskills.component';



const routes: Routes = [
  {
    path: '', component: AdminComponent, children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'resume', component: ResumeAdminComponent },
      { path: 'resume/featured-skills', component: FeaturedskillsComponent },
      { path: 'profile', component: ProfileComponent },
      { path: 'profile/profilepics', component: PhotoaddComponent },
      { path: 'posts/list', component: PostlistComponent },
      { path: 'posts/new', component: AddpostComponent },
      { path: 'posts/new/photo', component: AddphotopostComponent },
      { path: 'posts/managecomments', component: CommentsComponent },
      { path: 'posts/edit', component: EditPostComponent },
      { path: 'product/list', component: ProductListComponent },
      { path: 'product/edit', component: ProductEditComponent },
      { path: 'tag/list', component: TaglistComponent },
      { path: 'product/new', component: ProductAddComponent },
      { path: 'product/new/photo', component: ProductphotoComponent },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'faqs', component: FaqsComponent },
      { path: 'messages', component: MessagesComponent },
      { path: 'reviews', component: ReviewsComponent }

    ]
  }


];

export const AdminRoutes = RouterModule.forChild(routes);
