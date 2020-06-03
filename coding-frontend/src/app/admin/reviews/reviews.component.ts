import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
// import { NbDialogService  } from "@nebular/theme";
import { ReviewService } from "../../_services/review.service";
// import { UserService } from "../../_services/user.service";
import {  Review } from "../../_models/Review";

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.sass']
})
export class ReviewsComponent implements OnInit {

  @ViewChild('dialogAdd',{ static: true }) dialogAdd: TemplateRef<any>

  reviews: Review[] = []

  constructor(
    // private userService: UserService,
    // private dialogService: NbDialogService,
    private reviewService: ReviewService ) { }

  ngOnInit() {

  }

  new() {
    this.reviewService.newReview()
    .subscribe((review: Review) => {
      review.url = "http://localhost:4200/pages/review/referal?id="+review.id
      this.reviews.push(review)
    })
    console.log(this.reviews)
  }



}
