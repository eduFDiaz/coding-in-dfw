import { Component, OnInit, TemplateRef } from '@angular/core';
import { NbDialogService  } from "@nebular/theme";

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.sass']
})
export class ReviewsComponent implements OnInit {



  constructor(private dialogService: NbDialogService ) { }

  ngOnInit() {
  }

  new() {
    
  }

}
