
import { NbSidebarModule } from '@nebular/theme';
import { AlertifyService } from './_Services/alertify.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { FooterComponent } from './footer/footer.component';
import { ContactComponent } from './contact/contact.component';
import { PostListComponent } from './blog/post-list/post-list.component';
import { PostComponent } from './blog/post/post.component';
import { ResumeComponent } from './resume/resume.component';
import { ProjectsComponent } from './projects/projects.component';
import { ServicesComponent } from './services/services.component';
import { HomeComponent } from './home/home.component';
import { ProjectsListComponent } from './projects/projects-list/projects-list.component';

import { AuthService } from './_Services/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { AdminModule } from './admin/admin.module';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    PortfolioComponent,
    FooterComponent,
    ContactComponent,
    PostListComponent,
    PostComponent,
    ResumeComponent,
    ProjectsComponent,
    ServicesComponent,
    HomeComponent,
    ProjectsListComponent,

  ],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule, FormsModule, AdminModule, NbSidebarModule.forRoot(),],
  providers: [AlertifyService, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
