
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule } from '@angular/forms';
import { AdminModule } from './admin/admin.module';
import { AuthService } from './_services/auth.service';
import { AlertService } from './_services/alert.service';
import { UserService } from './_services/user.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbSidebarModule, NbToastrModule } from '@nebular/theme';
import { PagesModule } from './pages/pages.module';
import { ErrorInterceptorService } from './_services/error-interceptor.service';
import { JwtInterceptor } from './_helpers/jwt.interceptor';




@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    NbSidebarModule.forRoot(),
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    PagesModule,
    AdminModule,
    AppRoutingModule,
  ],
  providers: [AlertService, AuthService, UserService,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
