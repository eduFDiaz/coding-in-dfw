import { Component, OnInit, TemplateRef } from '@angular/core';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';


import { Router, RouterLink } from '@angular/router';


import { NbDialogService } from '@nebular/theme';
import { RequirementAddComponent } from './requirement-add/requirement-add.component';
import { Product } from 'src/app/_models/Product';
import { Requirement } from 'src/app/_models/Requirement';
import { ProductService } from 'src/app/_services/product.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertService } from 'src/app/_services/alert.service';
import { ProductphotoAddComponent } from './productphoto-add/productphoto-add.component';


@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss']
})
export class ProductAddComponent implements OnInit {

  public Editor = ClassicEditor;

  spinner = false

  selected: any

  productaddSpinner = false

  forUser: number

  requirements: Requirement[] = []

  product: Product = {
    name: '',
    productDescription: '',
    url: '',
    type: '',
    userId: 0,
    clientName: '',
    industry: '',
    requirementId: null,
    size: null,
    text: '',
    projectIntro: '',
    shortResume: ''

  }

  constructor(
    private route: Router,
    private productService: ProductService, private user: UserService, private toast: AlertService,
    private dialogService: NbDialogService) { }

  ngOnInit() {
    this.product.userId = this.user.getCurrentUserId()
  }

  newProduct() {
    let arr = []
    this.spinner = true
    console.log(this.product)
    if (this.requirements.length == 0) {
      let emptyreq: Requirement = {
        description: "This product doesnt have any requirements"
      }
      this.productService.addRequirement(emptyreq).subscribe((result) => {
        arr.push(result.id)
        this.requirements = arr
      })
      this.product.requirementId = this.requirements
    }
    this.productService.addProduct(this.product).subscribe(
      value => {
        this.spinner = false;
        this.dialogService.open(ProductphotoAddComponent, { closeOnBackdropClick: true })
          .onClose.subscribe((result) => {
            console.log(result)
          })

        this.toast.showToast('bottom-left', 'success', 'Product Added!', 'Your product was succefully added')
        this.route.navigate(['/product/list'])
      },
      error => {
        this.spinner = false;
        this.toast.showToast('top-right', 'danger', 'There was an error', error.status + ' ' + error.statusText)
      }
    );
  }

  openAddRequirementDialog() {
    this.dialogService.open(RequirementAddComponent, { closeOnBackdropClick: true }).onClose.subscribe(
      (data) => {
        console.log(data)
        this.requirements.push(data)
      }
    )
  }

  generateArrayToCreate(obj) {
    return Object.keys(obj).map((key) => {
      return { key: key, value: obj[key] }
    }).filter((field: any) => field.key !== 'id' && field.key !== 'dateCreated' && field.key !== 'dateModified')
  }

  addPhotoLink() {
    this.dialogService.open(ProductphotoAddComponent, { closeOnBackdropClick: true })
      .onClose.subscribe((result) => {
        console.log(result)
      })
  }

}
