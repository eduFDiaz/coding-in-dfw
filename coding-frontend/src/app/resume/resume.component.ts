import { Component, OnInit } from '@angular/core';
import { UserService } from '../_Services/user.service';
import { User } from '../_Models/User';

@Component({
  selector: 'app-resume',
  templateUrl: './resume.component.html',
  styleUrls: ['./resume.component.sass']
})
export class ResumeComponent implements OnInit {

  userData: User;
  constructor(private user: UserService) { }

  ngOnInit() {
    this.user.getUserInfo().subscribe((user: User) => {
      this.userData = user;
    }, error => {
      console.log(error.error.message);
    }
    )
  }

}
