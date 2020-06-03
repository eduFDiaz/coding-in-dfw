import { Component, OnInit } from '@angular/core';
import { Review } from '../../_models/Review'
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.sass']
})
export class ReviewComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute) { }

  reviewId: string

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((result) => {
      this.reviewId = result['id']
      console.log(this.reviewId)
    })
  }

}
