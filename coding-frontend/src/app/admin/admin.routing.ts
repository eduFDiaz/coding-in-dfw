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



const routes: Routes = [
  {
    path: '', component: AdminComponent, children: [
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'resume', component: ResumeAdminComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
      { path: 'profile', component: ProfileComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
      { path: 'profile/profilepics', component: PhotoaddComponent, pathMatch: 'full' },
      {
        path: 'posts/list',
        component: PostlistComponent,
        pathMatch: 'full',
        canActivate: [AuthGuardService],
      },
      {
        path: 'posts/new',
        component: AddpostComponent,
        pathMatch: 'full',
        canActivate: [AuthGuardService],
      },
      {
        path: 'product/list',
        component: ProductListComponent,
        pathMatch: 'full',
        canActivate: [AuthGuardService]
      },
      {
        path: 'tag/list',
        component: TaglistComponent,
        pathMatch: 'full',
        canActivate: [AuthGuardService]
      },
      {
        path: 'product/new',
        component: ProductAddComponent,
        pathMatch: 'full',
        canActivate: [AuthGuardService]
      }
    ]
  }
];

export const AdminRoutes = RouterModule.forChild(routes);
