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
import { Interest } from 'src/app/_models/Interest';


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
  interests: Interest[]
  avatar: string

  constructor(
    private user: UserService,
    private resume: ResumeService) { }

  ngOnInit() {
    this.userData = JSON.parse(localStorage.getItem('userdata'))
    this.getData()
    this.user.getAllUserPhotos().subscribe((result: any) => {
      this.avatar = result.filter((item: any) => item.isMain == true)[0].url
    })
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
    this.resume.getInterests(this.userData.id).subscribe((data) => {
      this.interests = data
    })

  }




}