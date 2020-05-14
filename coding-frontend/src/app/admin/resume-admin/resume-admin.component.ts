import { Component, OnInit, TemplateRef } from '@angular/core';

import { NbDialogService } from '@nebular/theme';
import { Skill } from 'src/app/_models/Skill';
import { WorkExperience } from 'src/app/_models/WorkExperience';
import { Education } from 'src/app/_models/Education';
import { Project } from 'src/app/_models/Project';
import { Award } from 'src/app/_models/Award';
import { AlertService } from 'src/app/_services/alert.service';
import { ResumeService } from 'src/app/_services/resume.service';
import { UserService } from 'src/app/_services/user.service';

import resolvePropsSimple from '../../_helpers/resolveProps'
import { Interest } from 'src/app/_models/Interest';



@Component({
  selector: 'app-resume-admin',
  templateUrl: './resume-admin.component.html',
  styleUrls: ['./resume-admin.component.scss']
})
export class ResumeAdminComponent implements OnInit {

  langs: any
  educations: any
  awards: any
  skills: Skill[]
  projects: any
  workExperiences: any
  interests: Interest[]

  newWe: WorkExperience = {
    title: '',
    company: '',
    dateRange: '',
    resume: '',
    userId: this.user.getCurrentUserId()
  }

  newEducation: Education = {
    title: '',
    schoolName: '',
    dateRange: '',
    userId: this.user.getCurrentUserId()
  }

  newProject: Project = {
    title: '',
    resume: '',
    type: '',
    userId: this.user.getCurrentUserId()
  }

  newAward: Award = {
    title: '',
    year: 0,
    company: '',
    userId: this.user.getCurrentUserId()
  }

  newlang = {
    name: '',
    userId: this.user.getCurrentUserId()
  }


  newSkill: Skill = {
    title: '',
    userId: this.user.getCurrentUserId()
  }

  newInterest: Interest = {
    title: '',
    userId: this.user.getCurrentUserId()
  }

  spinner = false

  constructor(private alert: AlertService, private resume: ResumeService, private user: UserService, private dialogService: NbDialogService) { }

  ngOnInit() {
    this.getData()
  }

  generateArray(obj) {
    return Object.keys(obj).map((key) => {
      return { key: key, value: obj[key] }
    }).filter((field: any) => field.key !== 'id' && field.key !== 'userId' && field.key !== 'dateCreated' && field.key !== 'dateModified')
  }

  generateArrayToCreate(obj) {
    return Object.keys(obj).map((key) => {
      return { key: key, value: obj[key] }
    }).filter((field: any) => field.key !== 'userid' && field.key !== 'userId' && field.key !== 'dateCreated' && field.key !== 'dateModified')
  }

  getData() {
    this.resume.getLanguages(this.user.getCurrentUserId()).subscribe((data) => {
      this.langs = data;
      console.log(this.langs)
    })
    this.resume.getEducation(this.user.getCurrentUserId()).subscribe((data) => {
      this.educations = data;
    })
    this.resume.getSkills(this.user.getCurrentUserId()).subscribe((data) => {
      this.skills = data;
    })
    this.resume.getProjects(this.user.getCurrentUserId()).subscribe((data) => {
      this.projects = data;
    })
    this.resume.getAwards(this.user.getCurrentUserId()).subscribe((data) => {
      this.awards = data;
    })
    this.resume.getWe(this.user.getCurrentUserId()).subscribe((data) => {
      this.workExperiences = data;
    })
    this.resume.getInterests(this.user.getCurrentUserId()).subscribe((interests) => {
      this.interests = interests
    })



  }

  openDeleteDialog(dialog: TemplateRef<any>, data: any, kind: any) {

    this.dialogService.open(dialog, {
      context: {
        object: data,
        type: kind
      }
    }).onClose.subscribe((result) => {
      console.log(result)
      switch (result) {
        case 'deletelanguage':
          this.resume.deleteLanguage(data.id).subscribe((result) => {
            this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Your language was deleted')
          })
          this.langs = this.langs.filter((obj: any) => obj.id !== data.id)
          break;

        case 'interest':
          this.resume.deleteInterest(data.id).subscribe((result) => {
            this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Your interest was deleted')
          })
          this.langs = this.interests.filter((obj: any) => obj.id !== data.id)
          break;

        case 'deleteeducation':
          this.resume.deleteEducation(data.id).subscribe((result) => {
            this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Your Education was deleted')
          })
          this.educations = this.educations.filter((obj: any) => obj.id !== data.id)
          break

        case 'deleteskill':
          this.resume.deleteSkill(data.id).subscribe((result) => {
            this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Your Skill was deleted')
          })
          this.skills = this.skills.filter((obj: any) => obj.id !== data.id)
          break

        case 'deleteproject':
          this.resume.deleteProject(data.id).subscribe((result) => {
            this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Your Project was deleted')
          })
          this.projects = this.projects.filter((obj: any) => obj.id !== data.id)
          break

        case 'deleteaward':
          this.resume.deleteAward(data.id).subscribe((result) => {
            this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Your Award was deleted')
          })
          this.awards = this.awards.filter((obj: any) => obj.id !== data.id)
          break

        case 'deletewe':
          this.resume.deleteWe(data.id).subscribe((result) => {
            this.alert.showToast('bottom-left', 'info', 'Delete Ok', 'Your WorkExperience was deleted')
          })
          this.workExperiences = this.workExperiences.filter((obj: any) => obj.id !== data.id)
          break


        default:
          break;
      }
    })
  }

  openEditDialog(dialog: TemplateRef<any>, data: any, kind: any) {
    const props = resolvePropsSimple(data)
    this.dialogService.open(dialog, {
      context: {
        object: data,
        type: kind,
        props: this.generateArray(data)
      }
    }).onClose.subscribe((info) => {

      switch (info.type) {
        case 'education':
          this.resume.updateEducation(info.body.id, info.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Update', 'Education Updated')
            }
          )
          const Educpos = this.educations.map((item) => {
            return item.id
          }).indexOf(info.body.id)
          this.educations[Educpos] = info.body
          break;

        case 'interest':
          this.resume.updateInterest(info.body.id, info.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Update', 'Interests Updated')
            }
          )
          const Interestpos = this.interests.map((item) => {
            return item.id
          }).indexOf(info.body.id)
          this.interests[Interestpos] = info.body
          break;

        case 'language':
          this.resume.updateLanguage(info.body.id, info.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Update', 'Language Updated')
            }
          )
          const Langpos = this.langs.map((item) => {
            return item.id
          }).indexOf(info.body.id)
          this.langs[Langpos] = info.body
          break;

        case 'skill':
          this.resume.updateSkill(info.body.id, info.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Update', 'Skills Updated')
            }
          )
          const skillPos = this.skills.map((item) => {
            return item.id
          }).indexOf(info.body.id)
          this.skills[skillPos] = info.body
          break;

        case 'project':
          this.resume.updateProject(info.body.id, info.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Update', 'Projects Updated')
            }
          )
          const projectPos = this.projects.map((item) => {
            return item.id
          }).indexOf(info.body.id)
          this.projects[projectPos] = info.body
          break;

        case 'award':
          this.resume.updateAward(info.body.id, info.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Update', 'Awards Updated')
            }
          )
          const awardPos = this.awards.map((item) => {
            return item.id
          }).indexOf(info.body.id)
          this.awards[awardPos] = info.body
          break;

        case 'we':
          this.resume.updateWe(info.body.id, info.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Update', 'Work experiences updated')
            }
          )
          const wePos = this.workExperiences.map((item) => {
            return item.id
          }).indexOf(info.body.id)
          this.workExperiences[wePos] = info.body
          break;



        default:
          break;
      }

    })
  }

  createDialogGeneric(dialog: TemplateRef<any>, data: any, type: string) {
    this.dialogService.open(dialog, {
      context: {
        object: data,
        props: this.generateArrayToCreate(data),
        type: type
      }
    }).onClose.subscribe((result) => {
      console.log(result.type)
      console.log(result.body)
      switch (result.type) {
        case 'education':
          this.resume.addEducation(result.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Created', 'Education added!')
              this.educations.push(result)
            }
          )
          break;
        case 'interest':
          this.resume.addInterest(result.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Created', 'Interest added!')
              this.interests.push(result)
            }
          )
          break;

        case 'language':
          this.resume.addLanguage(result.body).subscribe(
            result => {
              console.log(result.body)
              this.alert.showToast('top-right', 'success', 'Created', 'Language added!')
              this.langs.push(result)
            }
          )
          break;

        case 'skill':
          this.resume.addSkill(result.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Created', 'Skill added!')
              this.skills.push(result)
            }
          )
          break;

        case 'award':
          this.resume.addAward(result.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Created', 'Award added!')
              this.awards.push(result)
            }
          )
          break;

        case 'project':
          this.resume.addProject(result.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Created', 'Project added!')
              this.projects.push(result)
            }
          )
          break;

        case 'we':
          this.resume.addWe(result.body).subscribe(
            result => {
              this.alert.showToast('top-right', 'success', 'Created', 'Work experience added!')
              this.workExperiences.push(result)
            }
          )
          break;

        default:
          break;
      }
    })
  }
}
