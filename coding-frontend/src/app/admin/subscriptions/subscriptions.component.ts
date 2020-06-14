import { Component, OnInit, TemplateRef } from '@angular/core';
import { SubscriptionService } from 'src/app/_services/subscription.service';
import { Subscriber } from 'src/app/_models/Subscriber';
import { NbDialogService } from '@nebular/theme';
import { AlertService } from 'src/app/_services/alert.service';


@Component({
  selector: 'app-subscriptions',
  templateUrl: './subscriptions.component.html',
  styleUrls: ['./subscriptions.component.sass']
})
export class SubscriptionsComponent implements OnInit {

  subscribers: Subscriber[]

  spinner: boolean

  newSub: string

  constructor(
    private alert: AlertService,
    private dialogService: NbDialogService,
    private subsService: SubscriptionService) { }

  ngOnInit() {
    this.spinner = true
    this.subsService.getAll().subscribe((subscribers: Subscriber[]) => {
      this.spinner = false
      this.subscribers = subscribers
    })
  }

  openDeleteDialog(dialog: TemplateRef<any>, id: string) {
    this.dialogService.open(dialog, {
      context: {
        data: id
      }
    })
  }

  openAddDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog)
  }

  deleteSubscriber(id: string) {
    this.spinner = true
    this.subsService.unsubscribe(id).subscribe(() => {
      this.spinner = false
      this.subscribers = this.subscribers.filter((item: any) => item.id !== id)
      this.alert.showToast('top-right', 'info', 'Deleted', 'The email was removed from list.')
    }, error => {
      this.alert.showToast('top-right', 'danger', 'Error!' + error, 'Something went wrong!')
    })
  }

  addSubs(data: Subscriber) {
    this.spinner = true
    this.subsService.newSubscriber(data).subscribe((result: Subscriber) => {
      this.subscribers.push(result)
      this.spinner = false
    })
  }

}
