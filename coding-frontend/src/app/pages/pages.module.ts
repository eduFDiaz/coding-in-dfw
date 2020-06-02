import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NavbarComponent } from './navbar/navbar.component'
import { ProjectsListComponent } from './projects/projects-list/projects-list.component';
import { PostListComponent } from './blog/post-list/post-list.component';
import { ServicesComponent } from './services/services.component';
import { ContactComponent } from './contact/contact.component';
import { ProjectsComponent } from './projects/projects.component';
import { ResumeComponent } from './resume/resume.component';
import { PortfolioComponent } from './portfolio/portfolio.component';

import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostComponent } from './blog/post/post.component';
import { AppPagesComponent } from './app-pages.component';
import { FooterComponent } from './footer/footer.component';
import { AppPagesRoutes } from './app-pages.routing';
import { FormsModule } from '@angular/forms';
import { SortByPipe } from '../_pipes/sort-by.pipe';

@NgModule({
  declarations: [HomeComponent,
    ProjectsListComponent,
    PostListComponent,
    ServicesComponent,
    ContactComponent,
    ProjectsComponent,
    ResumeComponent,
    PortfolioComponent,
    PostComponent,
    NavbarComponent,
    AppPagesComponent,
    FooterComponent,
    SortByPipe
  ],
  imports: [
    CommonModule, FormsModule,
    RouterModule, AppPagesRoutes
  ],

})
export class PagesModule { }
