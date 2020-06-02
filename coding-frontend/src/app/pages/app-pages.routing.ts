import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AppPagesComponent } from './app-pages.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { ServicesComponent } from './services/services.component';
import { ResumeComponent } from './resume/resume.component';
import { PostListComponent } from './blog/post-list/post-list.component';
import { PostComponent } from './blog/post/post.component';
import { ProjectsListComponent } from './projects/projects-list/projects-list.component';
import { ProjectsComponent } from './projects/projects.component';
import { ContactComponent } from './contact/contact.component';


const routes: Routes = [
  {
    path: '', component: AppPagesComponent, children: [
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'portfolio', component: PortfolioComponent, pathMatch: 'full' },
      { path: 'services', component: ServicesComponent, pathMatch: 'full' },
      { path: 'resume', component: ResumeComponent, pathMatch: 'full' },
      { path: 'blog', component: PostListComponent, pathMatch: 'full' },
      { path: 'blog/post/:id', component: PostComponent, pathMatch: 'full' },
      { path: 'projects', component: ProjectsListComponent, pathMatch: 'full' },
      { path: 'projects/:id', component: ProjectsComponent, pathMatch: 'full' },
      { path: 'contact', component: ContactComponent, pathMatch: 'full' },
    ]
  }

];

export const AppPagesRoutes = RouterModule.forChild(routes);
