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
import { ProductphotoAddComponent } from './products/product-add/productphoto-add/productphoto-add.component';



const routes: Routes = [
  {
    path: '', component: AdminComponent, children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'resume', component: ResumeAdminComponent },
      { path: 'profile', component: ProfileComponent, pathMatch: 'full' },
      { path: 'profile/profilepics', component: PhotoaddComponent },
      { path: 'posts/list', component: PostlistComponent },
      { path: 'posts/new', component: AddpostComponent },
      { path: 'posts/managecomments', component: CommentsComponent },
      { path: 'product/list', component: ProductListComponent },
      { path: 'tag/list', component: TaglistComponent },
      { path: 'product/new', component: ProductAddComponent },
      { path: 'product/new/picture', component: ProductphotoAddComponent },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'faqs', component: FaqsComponent, pathMatch: 'full' }
    ]
  }


];

export const AdminRoutes = RouterModule.forChild(routes);
