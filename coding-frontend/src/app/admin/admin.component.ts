import { Component, OnInit } from '@angular/core';

import { AuthService } from '../_services/auth.service'
import { NbMenuItem } from '@nebular/theme';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass'],

})
export class AdminComponent implements OnInit {

  // currentUser: User

  constructor(private auth: AuthService) {
    // this.currentUser = this.auth.currentUserValue

  }
  ngOnInit() {
    this.auth.currentLoginStatus.subscribe(result => this.isLoggedIn = result)
  }

  title = 'coding-admin';

  isLoggedIn: boolean;

  items: NbMenuItem[] = [
    {
      title: 'Home',
      link: '/admin/profile',
      icon: 'home',
    },
    {
      title: 'Posts',
      icon: 'book',
      expanded: false,
      children: [
        {
          title: 'Tags list',
          icon: 'layers',
          link: '/admin/tag/list',

        },
        {
          title: 'Post list',
          link: '/admin/posts/list',
          icon: 'list'
        },
        {
          title: 'Write new Post',
          link: '/admin/posts/new',
          icon: 'plus-outline'
        },
        {
          title: 'Comments',
          link: '/admin/posts/managecomments',
          icon: 'heart-outline'
        }

      ]
    },

    {
      title: 'Products',
      icon: 'cube',
      expanded: false,
      children: [
        {
          title: 'Product list',
          icon: 'list',
          link: '/admin/product/list'
        },
        {
          title: 'I have new product',
          icon: 'plus',
          link: '/admin/product/new'
        }
      ]
    },
    {
      title: 'Resume',
      icon: 'book-open-outline',
      expanded: false,
      children: [{
        title: 'Featured Skills',
        icon: 'star',
        link: '/admin/resume/featured-skills'
      },
      {
        title: 'Edit your resume',
        icon: 'book-open-outline',
        link: '/admin/resume',
      }
      ]

    },
    {
      title: 'FAQs',
      icon: 'question-mark-outline',
      expanded: false,
      link: '/admin/faqs'
    },
    {
      title: 'Direct Messages',
      icon: 'message-square-outline',
      expanded: false,
      link: '/admin/messages'
    },
    {
      title: 'Reviews',
      icon: 'file-text-outline',
      expanded: false,
      link: '/admin/reviews'
    }

  ];


}
