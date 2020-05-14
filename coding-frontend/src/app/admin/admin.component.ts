import { Component, OnInit } from '@angular/core';

import { AuthService } from '../_services/auth.service'
import { NbMenuItem } from '@nebular/theme';
import { User } from '../_models/User';
import { BehaviorSubject, Observable } from 'rxjs';



@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass'],

})
export class AdminComponent implements OnInit {

  currentUser: User

  constructor(private auth: AuthService) {
    // this.auth.getUser().subscribe(x => this.currentUser = x)
    this.auth.changeCurrentDisplayMode()

  }
  ngOnInit() {


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
          url: '/admin/tag/list'

        },
        {
          title: 'Post list',
          url: '/admin/posts/list',
          icon: 'list'
        },
        {
          title: 'Write new Post',
          url: 'admin/posts/new',
          icon: 'plus-outline'
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
          url: '/admin/product/list'
        },
        {
          title: 'I have new product',
          icon: 'plus',
          url: '/admin/product/new'
        }
      ]
    },
    {
      title: 'Resume',
      icon: 'book-open-outline',
      expanded: false,
      url: '/admin/resume'

    }

  ];


}
