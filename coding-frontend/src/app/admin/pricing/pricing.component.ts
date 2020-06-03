import { Component, OnInit } from '@angular/core';


import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.sass']
})
export class PricingComponent implements OnInit {

  public Editor = ClassicEditor;

  constructor() { }

  ngOnInit() {
  }

}
