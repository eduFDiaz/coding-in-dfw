
import {
  NbListModule,
  NbSidebarService, NbWindowModule, NbSidebarModule, NbThemeModule, NbSpinnerModule, NbUserModule, NbRadioModule, NbDialogModule,
  NbLayoutModule, NbBadgeModule, NbButtonModule, NbCheckboxModule, NbMenuService, NbMenuModule, NbCardModule, NbInputModule, NbAlertModule, NbIconModule, NbToastrModule, NbDialogService, NbDialogRef,
} from '@nebular/theme';

// import { BrowserModule } from '@angular/platform-browser';

import { NgModule } from '@angular/core';

import { FileUploadModule } from 'ng2-file-upload'

import { AdminComponent } from './admin.component';
import { RouterModule } from '@angular/router';
import { AdminRoutes } from './admin.routing';
import { HeaderModule } from './header/header.module';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';

import { NbEvaIconsModule } from '@nebular/eva-icons';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
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
import { FaqsComponent } from './faqs/faqs.component';
import { MessagesComponent } from './messages/messages.component';
import { ReviewsComponent } from './reviews/reviews.component';



@NgModule({
  providers: [NbSidebarService, NbMenuService, AlertService],
  declarations: [AdminComponent,
    LoginComponent,
    ProfileComponent,
    ResumeAdminComponent,
    PhotoaddComponent,
    FaqsComponent,
    MessagesComponent,
    ReviewsComponent,
    ],
  imports: [
    CommonModule,
    NbToastrModule.forRoot(),
    FileUploadModule,
    ProductsModule,
    PostModule,
    NbRadioModule,
    NbBadgeModule,
    NbListModule,
    NbUserModule,
    NbLayoutModule,
    NbWindowModule.forRoot(),
    NbDialogModule.forChild(),
    NbEvaIconsModule,
    HeaderModule,
    NbInputModule,
    NbSidebarModule,
    NbButtonModule,
    HttpClientModule,
    NbCardModule,
    NbIconModule,
    NbAlertModule,
    CKEditorModule,
    NbCheckboxModule,
    FormsModule,
    NbSpinnerModule,
    NbMenuModule.forRoot(),
    NbThemeModule.forRoot({ name: 'default' }),
    AdminRoutes,
  ], entryComponents: [PhotoaddComponent]
})
export class AdminModule { }
