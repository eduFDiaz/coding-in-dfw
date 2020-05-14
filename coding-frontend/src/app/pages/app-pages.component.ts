import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';
import { Language } from '../_models/Language';
import { Education } from '../_models/Education';
import { Skill } from '../_models/Skill';
import { Project } from '../_models/Project';
import { Award } from '../_models/Award';
import { WorkExperience } from '../_models/WorkExperience';
import { ResumeService } from '../_services/resume.service';
import { User } from '../_models/User';
import { ProfileData } from '../_models/ProfileData';

@Component({
  selector: 'app-pages',
  templateUrl: './app-pages.component.html',
  // styleUrls: ['./app.component.sass']
})
export class AppPagesComponent implements OnInit {
  title = 'coding-frontend';

  userData: User

  constructor(private user: UserService, private resume: ResumeService) {

    this.user.getAllUsers().subscribe((userdata) => {
      this.userData = userdata[0]
      localStorage.setItem('userdata', JSON.stringify(this.userData))
    })

  }


  ngOnInit() {

  }

}