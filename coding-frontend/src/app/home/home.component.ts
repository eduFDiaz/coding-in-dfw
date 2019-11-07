import { AlertifyService } from './../_Services/alertify.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {

  constructor(private alertify: AlertifyService) { }

  ngOnInit() {
  }
}
