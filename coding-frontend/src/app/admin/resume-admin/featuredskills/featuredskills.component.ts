import { Component, OnInit, TemplateRef, ViewChild, ElementRef } from '@angular/core';
import { FeaturedSkill } from 'src/app/_models/FeaturedSkill';
import { FeaturedSkillsService } from 'src/app/_services/featured-skills.service';
import { AlertService } from 'src/app/_services/alert.service';
import { NbDialogService } from '@nebular/theme';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-featuredskills',
  templateUrl: './featuredskills.component.html',
  styleUrls: ['./featuredskills.component.sass']
})
export class FeaturedskillsComponent implements OnInit {



  spinner = false

  featuredSkills: FeaturedSkill[] = []

  constructor(
    private dialogService: NbDialogService,
    private alert: AlertService,
    private featuredSkillService: FeaturedSkillsService) { }

  ngOnInit() {
    this.featuredSkillService.getAllFeaturedSkills().subscribe((result: FeaturedSkill[]) => {

      this.featuredSkills = result
      console.log(this.featuredSkills)
    })


  }

  resolveItems(type: string) {
    let icons = []
    switch (type) {
      case 'Vanilla Javascript':
        icons = ["fab fa-js-square"]
        break;
      case 'Angular, React & Vue':
        icons = ["fab fa-angular mr-2", "fab fa-react mr-2", "fab fa-vuejs"]
        break;
      case 'NodeJS':
        icons = ["fab fa-node-js mr-2"]
        break;
      case 'Python & Django':
        icons = ["fab fa-python mr-2"]
        break;
      case 'PHP':
        icons = ["fab fa-php mr-2"]
        break;
      case 'Npm, Gulp & Grunt':
        icons = ["fab fa-npm mr-2", "fab fa-gulp mr-2", "fab fa-grunt"]
        break;
      case 'HTML & CSS':
        icons = ["fab fa-html5 mr-2", "fab fa-css3 mr-2"]
        break;
      case 'SASS & LESS':
        icons = ["fab fa-sass mr-2", "fab fa-less mr-2"]
        break;
      default:
        break;
    }
    return icons
  }

  createFeature(feature: FeaturedSkill) {
    this.spinner = true
    feature.icons = this.resolveItems(feature.title)
    this.featuredSkillService.newFeaturedSkill(feature).subscribe((newfeature: FeaturedSkill) => {
      this.alert.showToast('top-right', 'success', 'Ok', 'Created featured!')
      this.featuredSkills.push(newfeature)
    }, err => {
      this.alert.showToast('top-right', 'danger', 'Error' + err, 'Cant create')
    }, () => {
      console.log("complete callback")
      this.spinner = false

    })
  }

  deleteFeature(id: string) {
    this.featuredSkillService.deleteFeatureSkill(id).subscribe(() => {
      this.alert.showToast('top-right', 'info', 'Ok', 'Deleted featured!')
      this.featuredSkills = this.featuredSkills.filter(item => item.id !== id)
    })
  }

  editFeature(id: string, data: FeaturedSkill) {
    data.icons = this.resolveItems(data.title)
    this.featuredSkillService.editFeatureSkill(id, data).subscribe((editedFeature: FeaturedSkill) => {
      this.alert.showToast('top-right', 'info', 'Ok', 'Updated featured!')
      console.log(editedFeature)
    })
  }

  openNewDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog)
  }

  openDeleteDialog(dialog: TemplateRef<any>, id: string) {
    this.dialogService.open(dialog, {
      context: {
        id: id
      }
    })
  }

  openEditDialog(dialog: TemplateRef<any>, feature: FeaturedSkill) {
    this.dialogService.open(dialog, {
      context: {
        data: feature
      }
    })
  }






}
