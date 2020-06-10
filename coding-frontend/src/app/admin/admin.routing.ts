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
import { GuestGuard } from '../_services/guest-guard.service';



const routes: Routes = [
  {
    path: '', component: AdminComponent, children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'resume', component: ResumeAdminComponent, canActivate: [AuthGuardService] },
      { path: 'resume/featured-skills', component: FeaturedskillsComponent, canActivate: [AuthGuardService] },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuardService] },
      { path: 'profile/profilepics', component: PhotoaddComponent, canActivate: [AuthGuardService] },
      { path: 'posts/list', component: PostlistComponent, canActivate: [AuthGuardService] },
      { path: 'posts/new', component: AddpostComponent, canActivate: [AuthGuardService] },
      { path: 'posts/new/photo', component: AddphotopostComponent, canActivate: [AuthGuardService] },
      { path: 'posts/managecomments', component: CommentsComponent, canActivate: [AuthGuardService] },
      { path: 'posts/edit', component: EditPostComponent, canActivate: [AuthGuardService] },
      { path: 'product/list', component: ProductListComponent, canActivate: [AuthGuardService] },
      { path: 'product/edit', component: ProductEditComponent, canActivate: [AuthGuardService] },
      { path: 'tag/list', component: TaglistComponent, canActivate: [AuthGuardService] },
      { path: 'product/new', component: ProductAddComponent, canActivate: [AuthGuardService] },
      { path: 'product/new/photo', component: ProductphotoComponent, canActivate: [AuthGuardService] },
      { path: 'faqs', component: FaqsComponent, canActivate: [AuthGuardService] },
      { path: 'messages', component: MessagesComponent, canActivate: [AuthGuardService] },
      { path: 'reviews', component: ReviewsComponent, canActivate: [AuthGuardService] }

    ]
  }



];

export const AdminRoutes = RouterModule.forChild(routes);
