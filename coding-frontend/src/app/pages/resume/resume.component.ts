import { Component, OnInit } from '@angular/core';
import { ResumeService } from 'src/app/_services/resume.service';
import { UserService } from 'src/app/_services/user.service';
import { Language } from 'src/app/_models/Language';
import { Education } from 'src/app/_models/Education';
import { Skill } from 'src/app/_models/Skill';
import { Project } from 'src/app/_models/Project';
import { Award } from 'src/app/_models/Award';
import { WorkExperience } from 'src/app/_models/WorkExperience';
import { ProfileData } from 'src/app/_models/ProfileData';


@Component({
  selector: 'app-resume',
  templateUrl: './resume.component.html',
  styleUrls: ['./resume.component.sass']
})
export class ResumeComponent implements OnInit {

  profileData: ProfileData

  userData: any
  languages: any;
  educations: any;
  skills: Skill[];
  projects: any;
  awards: any;
  workExperiences: any;

  constructor(private resume: ResumeService) { }

  ngOnInit() {
    this.userData = JSON.parse(localStorage.getItem('userdata'))
    this.getData()
  }

  getData() {
    this.resume.getLanguages(this.userData.id).subscribe((data) => {
      this.languages = data
    })
    this.resume.getEducation(this.userData.id).subscribe((data) => {
      this.educations = data
    })
    this.resume.getSkills(this.userData.id).subscribe((data) => {
      this.skills = data
    })
    this.resume.getProjects(this.userData.id).subscribe((data) => {
      this.projects = data
    })
    this.resume.getAwards(this.userData.id).subscribe((data) => {
      this.awards = data
    })
    this.resume.getWe(this.userData.id).subscribe((data) => {
      this.workExperiences = data
    })

  }




}