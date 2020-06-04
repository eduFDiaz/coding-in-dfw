import { Component, OnInit, ViewChild } from '@angular/core';
import { Review } from '../../_models/Review'
import { User } from '../../_models/User'
import { ActivatedRoute, Router } from '@angular/router'
import { ReviewService } from '../../_services/review.service'
import { AlertifyServiceService } from 'src/app/_services/alertify-service.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.sass']
})
export class ReviewComponent implements OnInit {

  @ViewChild('contactForm', { static: true}) contactForm

  constructor(
    private router: Router,
    private alertService: AlertifyServiceService,
    private reviewService: ReviewService,
    private activatedRoute: ActivatedRoute) { }

  reviewId: string
  user: User;

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((result) => {
      this.reviewId = result['id']
      console.log(this.reviewId)
    })
    this.user = JSON.parse(localStorage.getItem('userdata'));
  }

  sendReview(data: any) {
    console.log(data)
    this.reviewService.confirmReview(this.reviewId, data).subscribe((result) => {
      this.alertService.success("Thanks for your review!")
      console.log(result)
      this.contactForm.resetForm()
      this.router.navigate(['/home'])
    })
  }

}
