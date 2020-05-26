import { Component, OnInit } from '@angular/core';

import { NbDialogRef } from '@nebular/theme';
import { Requirement } from 'src/app/_models/Requirement';
import { AlertService } from 'src/app/_services/alert.service';
import { ProductService } from 'src/app/_services/product.service';




@Component({
  selector: 'app-requirement-add',
  templateUrl: './requirement-add.component.html',
  styleUrls: ['./requirement-add.component.scss']
})
export class RequirementAddComponent implements OnInit {

  spinner = false

  newReq: Requirement = {
    description: '',
    id: '',
  }

  constructor(private alert: AlertService, protected dialogRef: NbDialogRef<any>, private productService: ProductService) { }

  ngOnInit() {
  }

  newRequirement() {
    this.spinner = true
    console.log(this.newReq)
    this.productService.addRequirement(this.newReq).subscribe((result) => {
      this.dialogRef.close(result)
      this.alert.showToast('top-right', 'success', 'Please, select it from the requirement selector below', 'Your requirement was added!')
      console.log("ok")
    }, error => {
      console.log(error)
    })
  }
}
