
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

  constructor(private alert: AlertService, private user: UserService) { }

  ngOnInit() {
    this.userData = JSON.parse(localStorage.getItem('userdata'));
  }
}
