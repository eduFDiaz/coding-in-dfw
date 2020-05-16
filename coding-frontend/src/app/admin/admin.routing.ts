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



const routes: Routes = [
  {
    path: '', component: AdminComponent, canActivate: [AuthGuardService], children: [
      { path: 'resume', component: ResumeAdminComponent, pathMatch: 'full' },
      { path: 'profile', component: ProfileComponent, pathMatch: 'full' },
      { path: 'profile/profilepics', component: PhotoaddComponent, pathMatch: 'full' },
      { path: 'posts/list', component: PostlistComponent, pathMatch: 'full' },
      { path: 'posts/new', component: AddpostComponent, pathMatch: 'full' },
      { path: 'posts/managecomments', component: CommentsComponent, pathMatch: 'full' },
      { path: 'product/list', component: ProductListComponent, pathMatch: 'full' },
      { path: 'tag/list', component: TaglistComponent, pathMatch: 'full', },
      { path: 'product/new', component: ProductAddComponent, pathMatch: 'full' }
    ]
  },
  { path: 'login', component: LoginComponent, pathMatch: 'full' },
];

export const AdminRoutes = RouterModule.forChild(routes);
