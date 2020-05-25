import { Component, OnInit, Input } from '@angular/core';

import { FileUploader } from "ng2-file-upload";
import { ProductPhoto } from 'src/app/_models/ProductPhoto';

@Component({
  selector: 'app-productphoto-add',
  templateUrl: './productphoto-add.component.html',
  styleUrls: ['./productphoto-add.component.sass']
})
export class ProductphotoAddComponent implements OnInit {

  @Input() photos: ProductPhoto[];

  uploader: FileUploader;

  currentMainPhoto: ProductPhoto;

  hasBaseDropZoneOver: boolean;

  constructor() { }

  ngOnInit() {
  }

}
