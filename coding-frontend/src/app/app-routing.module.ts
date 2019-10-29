import { ServicesComponent } from './services/services.component';
import { ContactComponent } from './contact/contact.component';
import { ProjectsComponent } from './projects/projects.component';
import { ResumeComponent } from './resume/resume.component';
import { BlogComponent } from './blog/blog.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { NavbarComponent } from './navbar/navbar.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';


const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full'},
  {
    path: '',
    children: [
      { path: 'home', component: HomeComponent},
      { path: 'portfolio', component: PortfolioComponent},
      { path: 'services', component: ServicesComponent},
      { path: 'resume', component: ResumeComponent},
      { path: 'blog', component: BlogComponent},
      { path: 'projects', component: ProjectsComponent},
      { path: 'contact', component: ContactComponent}]
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
