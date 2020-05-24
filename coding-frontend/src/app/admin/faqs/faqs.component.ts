import { Component, OnInit, TemplateRef } from '@angular/core';
import { Faq } from 'src/app/_models/Faq';
import { FaqServiceService } from 'src/app/_services/faq-service.service';
import { NbDialogService } from '@nebular/theme';
import { AlertService } from 'src/app/_services/alert.service';
import { NgForm } from '@angular/forms'

@Component({
  selector: 'app-faqs',
  templateUrl: './faqs.component.html',
  styleUrls: ['./faqs.component.sass']
})
export class FaqsComponent implements OnInit {

  faqs: Faq[]

  constructor(private faqService: FaqServiceService, private dialogService: NbDialogService, private alert: AlertService) { }

  ngOnInit() {
    this.faqService.getFaqs().subscribe((result: any) => {
      this.faqs = result
      console.log(this.faqs)
    })

  }

  openDeleteDialog(dialog: TemplateRef<any>, data: Faq) {
    this.dialogService.open(dialog).onClose.subscribe((result) => {
      this.faqService.deleteFaq(data.id).subscribe((ok) => {
        this.faqs = this.faqs.filter((obj: any) => obj.id !== data.id)
        this.alert.showToast('top-right', 'info', 'Delete OK', 'Your FAQ was deleted')
      }, err => {
        this.alert.showToast('top-right', 'danger', 'Error!', 'Cant delete the FAq')
      })

    })
  }

  openEditDialog(dialog: TemplateRef<any>, data: any) {
    this.dialogService.open(dialog, { context: { object: data } }).onClose
      .subscribe((info) => {
        if (info) {
          this.faqService.editFaq(info.id, info.body).subscribe((ok) => {
            this.alert.showToast('top-right', 'success', 'Created OK', 'Your FAQ was created')
          }, err => {
            this.alert.showToast('top-right', 'danger', 'Error!', 'Cant edit the FAq')
          })
        }
      })
  }

  createDialogGeneric(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog).onClose.subscribe((result) => {
      console.log(result.type)
      console.log(result.body)

    })
  }

}
