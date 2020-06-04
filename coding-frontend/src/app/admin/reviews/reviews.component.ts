import { Component, OnInit, TemplateRef  } from '@angular/core';
import { NbDialogService  } from "@nebular/theme";
import { ReviewService } from "../../_services/review.service";
import { UserService } from "../../_services/user.service";
import {  Review } from "../../_models/Review";

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.sass']
})
export class ReviewsComponent implements OnInit {

  // @ViewChild('dialogAdd',{ static: true }) dialogAdd: TemplateRef<any>

  reviews: Review[] = []

  constructor(
    private userService: UserService,
    private dialogService: NbDialogService,
    private reviewService: ReviewService ) { }

  ngOnInit() {

  }

  new(data: any) {
    console.log(data)
    data.userid = this.userService.getCurrentUserId();
    this.reviewService.newDraftReview(data)
    .subscribe((review: Review) => {
      this.reviews.push(review)
    })
    console.log(this.reviews)
  }

  openNewDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog)
  }

  deleteReview(id: string) {
    this.reviewService.deleteReview(id).subscribe(() => {
      this.reviews = this.reviews.filter((item: Review) => item.id !== id)
    })
  }

}
