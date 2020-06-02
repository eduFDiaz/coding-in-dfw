
import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../_services/alert.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {

  userData: any
  avatar: any

  constructor(private alert: AlertService, private user: UserService) { }

  ngOnInit() {
    this.user.getAllUsers().subscribe((userdata) => {
      this.userData = userdata[0]
      localStorage.setItem('userdata', JSON.stringify(this.userData))
    })
    this.user.getAllUserPhotos().subscribe((result: any) => {
      this.avatar = result.filter((item: any) => item.isMain == true)[0].url
    })
  }
}
