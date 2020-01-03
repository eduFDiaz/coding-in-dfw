import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserService } from '../_Services/user.service';
import { User } from '../_Models/User';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit {
  @Input() userData: any;
  isDataLoading = false;
  private subscription: Subscription;

  constructor(private user: UserService) { }

  ngOnInit() {
    // tslint:disable-next-line: max-line-length
    this.subscription = this.user.userInfo$.subscribe(
      (user) => {
        this.userData = user;
      }
    );
    this.getUserInfo();
  }

  getUserInfo() {
    return this.user.getUserInfo().subscribe((data) => {
      this.userData = data;

    }, error => {
      console.log(error);
    });

  }


}
