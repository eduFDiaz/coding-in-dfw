import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from 'src/app/_services/subscription.service';
import { AlertifyServiceService } from 'src/app/_services/alertify-service.service';
import { Subscriber } from 'src/app/_models/Subscriber';

@Component({
  selector: 'app-unsubscribe',
  templateUrl: './unsubscribe.component.html',
  styleUrls: ['./unsubscribe.component.sass']
})
export class UnsubscribeComponent implements OnInit {

  subscribers: Subscriber[]
  unsbuEmail: string

  constructor(
    private alertify: AlertifyServiceService,
    private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.subscriptionService.getAll().subscribe((result: Subscriber[]) => {
      this.subscribers = result
      console.log(this.subscribers)
    })
  }

  deleteSubscription(fromForm: any) {
    let subsid = this.subscribers.filter((item: any) => item.email === fromForm.email)[0]
    if (subsid !== undefined) {
      this.subscriptionService.unsubscribe(subsid.id).subscribe(() => {
        this.alertify.success('Your mail was removed from the subsrciption list!')
      }, () => {
        this.alertify.error('Something may happen whit the backend :( ')
      })
    } else {
      this.alertify.error('Your mail doesn\'t exist on the subscribers lists')
    }
  }

}
