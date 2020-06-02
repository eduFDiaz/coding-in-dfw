import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from 'src/app/_models/User';



@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit {

  @Input() user: User
  @Input() avatar: any


  constructor() { }

  ngOnInit() {
  }



}
