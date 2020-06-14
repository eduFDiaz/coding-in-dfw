import { Component, OnInit, ViewChild } from '@angular/core';
import { PostService } from 'src/app/_services/post.service';

import { User } from 'src/app/_models/User';
import { Post } from 'src/app/_models/Post';
import { Subscriber } from 'src/app/_models/Subscriber';
import { SubscriptionService } from 'src/app/_services/subscription.service';
import { AlertifyServiceService } from 'src/app/_services/alertify-service.service';


@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.sass']
})
export class PostListComponent implements OnInit {

  loading: boolean

  currentUser: User
  posts: Post[]
  postResume: string

  newSubscriber: Subscriber = {
    email: ''
  }


  constructor(
    private alertify: AlertifyServiceService,
    private subscriptionService: SubscriptionService,
    private postService: PostService) { }

  ngOnInit() {
    this.loading = true
    this.currentUser = JSON.parse(localStorage.getItem('userdata'))
    this.postService.getUserPosts(this.currentUser.id).subscribe((posts: Post[]) => {
      this.posts = posts
      this.loading = false
    })

  }

  stripHtml(html: string) {
    var div = document.createElement("DIV");
    div.innerHTML = html;
    let cleanText = div.innerText;
    div = null; // prevent mem leaks
    return cleanText;
  }

  addSubscription(data: Subscriber) {
    console.log(data)
    this.subscriptionService.newSubscriber(data).subscribe(() => {
      this.alertify.success('Success!, you email was added to the subscribers list')

    }, () => {
      this.alertify.error('Failure')
    })
  }

}
