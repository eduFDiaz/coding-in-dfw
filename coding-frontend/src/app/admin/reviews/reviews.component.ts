import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NbDialogService } from "@nebular/theme";
import { ReviewService } from "../../_services/review.service";
import { UserService } from "../../_services/user.service";
import { AlertService } from '../../_services/alert.service'
import { Review } from "../../_models/Review";


@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.sass']
})
export class ReviewsComponent implements OnInit {

  spinner: boolean

  @ViewChild('selectForm', { static: true }) selectForm: TemplateRef<any>
  // @ViewChild('selectType',{ static: true }) selectType

  reviews: Review[] = []

  newReview: {
    email: '',
    company: ''
  }

  filterType: string

  getType(value: string) {
    this.filterType = value
    // return value
  }

  constructor(
    private alertService: AlertService,
    private userService: UserService,
    private dialogService: NbDialogService,
    private reviewService: ReviewService) { }

  ngOnInit() {
    this.reviewService.getAllReviews(this.userService.getCurrentUserId()).subscribe((result: Review[]) => {
      this.reviews = result

    })

  }

  new(data: any) {
    this.spinner = true
    data.userid = this.userService.getCurrentUserId();
    this.reviewService.newDraftReview(data)
      .subscribe((review: Review) => {
        this.reviews.push(review)
        this.alertService.showToast('top-right', 'success', 'Invitation Send!, now you have to wait for aprove it in this section before it shows on the front page', 'Your invitation was sent!')
      }, error => {

        this.alertService.showToast('top-right', 'danger', 'Invitation fail!', 'Your invitation was not send because:' + error
        )
      }, () => { this.spinner = false })

  }

  openDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog, {
      context: {
        data: this.newReview
      }
    })
  }

  openDeleteDialog(dialog: TemplateRef<any>, reviewid: string) {
    this.dialogService.open(dialog, {
      context: {
        id: reviewid
      }
    })
  }

  deleteReview(item: any) {
    this.spinner = true
    let id = item.id
    this.reviewService.deleteReview(id).subscribe(() => {
      this.alertService.showToast('top-right', 'info', 'Invitation delete!', 'The asociated review was deleted too')
    }, error => {
      this.alertService.showToast('top-right', 'danger', 'Deletion fails!', 'Your delete cant be preformed because' + error)
    }, () => {
      this.reviews = this.reviews.filter((item: any) => item.id !== id)
      this.spinner = false
    })
  }

  aproveReview(id: string) {
    this.spinner = true
    this.reviewService.publishReview(id).subscribe(() => {
      this.alertService.showToast('top-right', 'info', 'Review aproved!', 'Sent to the frontpage!')
    }, error => {

    }, () => {
      this.reviewService.getAllReviews(this.userService.getCurrentUserId()).subscribe((result: Review[]) => {
        this.reviews = result

      })
      this.spinner = false

    })
  }

}
