import { NgModule } from '@angular/core';

import { Routes, RouterModule } from '@angular/router';
import { AppPagesComponent } from './app-pages.component';
import { HomeComponent } from './home/home.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { ServicesComponent } from './services/services.component';
import { ResumeComponent } from './resume/resume.component';
import { PostListComponent } from './blog/post-list/post-list.component';
import { PostComponent } from './blog/post/post.component';
import { ProjectsListComponent } from './projects/projects-list/projects-list.component';
import { ProjectsComponent } from './projects/projects.component';
import { ContactComponent } from './contact/contact.component';

const pagesRoutes: Routes = [
  {
    path: 'pages', component: AppPagesComponent, children: [
      { path: 'home', component: HomeComponent },
      { path: 'portfolio', component: PortfolioComponent },
      { path: 'services', component: ServicesComponent },
      { path: 'resume', component: ResumeComponent },
      { path: 'blog', component: PostListComponent },
      { path: 'blog/:id', component: PostComponent },
      { path: 'projects', component: ProjectsListComponent },
      { path: 'projects/:id', component: ProjectsComponent },
      { path: 'contact', component: ContactComponent },
    ]
  }

];

@NgModule({
  imports: [
    RouterModule.forChild(pagesRoutes)
  ],
  exports: [
    RouterModule
  ]
})

export class PagesRouteRoutes { }
