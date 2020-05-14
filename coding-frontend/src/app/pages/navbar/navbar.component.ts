import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from 'rxjs';



@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.sass']
})
export class NavbarComponent implements OnInit {

  @Input() user: Object


  constructor() { }

  ngOnInit() {

  }



}
