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

  constructor(
    private alertify: AlertifyServiceService,
    private subscriptionService: SubscriptionService) { }

  ngOnInit() {
    this.subscriptionService.getAll().subscribe((result: Subscriber[]) => {
      this.subscribers = result
    })
  }

  deleteSubscription(email: string) {
    let subsid = this.subscribers.find(item => item.email == email).id
    this.subscriptionService.unsubscribe(subsid).subscribe(() => {
      this.alertify.success('Your mail was removed from the subsrciption list!')
    }, () => {
      this.alertify.error('Your mail was\'nt removed from the subsrciption list')
    })
  }

}
