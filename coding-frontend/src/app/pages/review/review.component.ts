import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'
import { ReviewService } from '../../_services/review.service'
import { AlertifyServiceService } from 'src/app/_services/alertify-service.service';
import { Review } from 'src/app/_models/Review';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.sass']
})
export class ReviewComponent implements OnInit {

  @ViewChild('contactForm', { static: true }) contactForm

  newRev = {
    name: '',
    body: ''
  }

  constructor(
    private router: Router,
    private alertService: AlertifyServiceService,
    private reviewService: ReviewService,
    private activatedRoute: ActivatedRoute) { }

  reviewId: string

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((result) => {
      this.reviewId = result['id']

    })

  }

  sendReview(data: any) {

    this.reviewService.confirmReview(this.reviewId, data).subscribe((result) => {
      this.alertService.success("Thanks for your review!")
      this.contactForm.resetForm()
      this.router.navigate(['/home'])
    })
  }

}
