
import {
  NbListModule,
  NbSidebarService, NbWindowModule, NbSidebarModule, NbThemeModule, NbSpinnerModule, NbUserModule, NbRadioModule, NbDialogModule,
  NbLayoutModule, NbButtonModule, NbCheckboxModule, NbMenuService, NbMenuModule, NbCardModule, NbInputModule, NbAlertModule, NbIconModule, NbToastrModule,
} from '@nebular/theme';

// import { BrowserModule } from '@angular/platform-browser';

import { NgModule } from '@angular/core';

import { FileUploadModule } from 'ng2-file-upload'

import { AdminComponent } from './admin.component';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';
import { HeaderModule } from './header/header.module';

import { NbEvaIconsModule } from '@nebular/eva-icons';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { PostService } from '../_services/post.service';
import { ResumeService } from '../_services/resume.service';
import { AuthGuardService } from '../_services/auth-guard.service';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { ProductsModule } from './products/products.module';
import { PostModule } from './post/post.module';
import { ResumeAdminComponent } from '../admin/resume-admin/resume-admin.component'
import { PhotoaddComponent } from './profile/photoadd/photoadd.component';
import { CommonModule } from '@angular/common';
import { AlertService } from '../_services/alert.service';


@NgModule({
  providers: [NbSidebarService, NbMenuService],
  declarations: [AdminComponent, LoginComponent, ProfileComponent, ResumeAdminComponent, PhotoaddComponent],
  imports: [
    CommonModule,
    FileUploadModule,
    ProductsModule,
    PostModule,
    NbRadioModule,
    NbListModule,
    NbUserModule,
    NbLayoutModule,
    NbWindowModule.forRoot(),
    NbDialogModule.forChild(),
    NbToastrModule.forRoot(),
    NbEvaIconsModule,
    HeaderModule,
    NbInputModule,
    NbSidebarModule,
    NbButtonModule,
    HttpClientModule,
    NbCardModule,
    NbIconModule,
    NbAlertModule,
    NbCheckboxModule,
    FormsModule,
    NbSpinnerModule,
    AdminRoutes,
    NbMenuModule.forRoot(),
    NbThemeModule.forRoot({ name: 'default' }),
  ], entryComponents: [PhotoaddComponent]
})
export class AdminModule { }
