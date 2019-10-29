import { ProjectsListComponent } from './projects/projects-list/projects-list.component';
import { PostListComponent } from './blog/post-list/post-list.component';
import { ServicesComponent } from './services/services.component';
import { ContactComponent } from './contact/contact.component';
import { ProjectsComponent } from './projects/projects.component';
import { ResumeComponent } from './resume/resume.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PostComponent } from './blog/post/post.component';


const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full'},
  {
    path: '',
    children: [
      { path: 'home', component: HomeComponent},
      { path: 'portfolio', component: PortfolioComponent},
      { path: 'services', component: ServicesComponent},
      { path: 'resume', component: ResumeComponent},
      { path: 'blog', component: PostListComponent},
      { path: 'blog/:id', component: PostComponent},
      { path: 'projects', component: ProjectsListComponent},
      { path: 'projects/:id', component: ProjectsComponent},
      { path: 'contact', component: ContactComponent}]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
