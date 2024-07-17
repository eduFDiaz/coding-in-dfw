import { Component, OnInit, Input, ViewChild, TemplateRef, OnChanges } from '@angular/core';
import { NbDialogRef, NbDialogService } from '@nebular/theme';
import { UserService } from 'src/app/_services/user.service';
import { ProductService } from 'src/app/_services/product.service';
import { AlertService } from 'src/app/_services/alert.service';
import { Product } from 'src/app/_models/Product';
import { Requirement } from 'src/app/_models/Requirement';
import { filter } from 'rxjs/operators';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { ActivatedRoute, Router } from '@angular/router';
// import { NgForm } from '@angular/forms'


@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {
  public Editor = ClassicEditor;

  @ViewChild('addRequirement', { static: true }) addRequirementDialog: TemplateRef<any>

  editSpinner = false

  product: any
  id: string

  pepe: boolean

  // requirements: any

  deleteSpinner = false;

  // elementAdded: boolean

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private alert: AlertService,
    private user: UserService,
    // protected dialogRef: NbDialogRef<any>,
    private dialogService: NbDialogService,
    private productService: ProductService,
  ) { }



  ngOnInit() {

    this.editSpinner = true
    this.route.queryParams.subscribe((params) => {
      this.id = params['forproduct']
    })
    this.productService.getProduct(this.id).subscribe((result) => {
      this.product = result

      this.editSpinner = false
    })


  }

  editItem(id: string, product: Product) {
    this.editSpinner = true
    product = this.product
    product.requirements = this.product.requirements.map(
      (item) => {
        return item.id
      }
    )

    this.productService.editProduct(id, product).subscribe(result => {
      this.editSpinner = false
      this.alert.showToast('bottom-left', 'info', 'Update ok', 'Requirements for this product updated!')
      this.router.navigate(['product/list'])
    })

  }

  removeRequirement(id: string) {
    this.productService.deleteRequirement(id).subscribe(result => {
      this.alert.showToast('top-right', 'info', 'Deleted', 'Requirement Deleted')
    })
    const index = this.product.requirements.map((item) => {
      return item.id
    }).indexOf(id)

    this.product.requirements.splice(index, 1);
    this.pepe = true
  }

  openAddRequirementDialog() {
    this.dialogService.open(this.addRequirementDialog, {
      context: {
        object: {}
      }, closeOnBackdropClick: false
    }).onClose.subscribe((data) => {
      this.newRequirement(data)


    })
  }


  newRequirement(item: string) {
    let newReq = {
      description: item
    }
    this.productService.addRequirement(newReq).subscribe((result) => {
      this.product.requirements.push(result)
      this.alert.showToast('bottom-left', 'success', 'Ok', 'Requirements for this product updated!')
    }, error => {

    })


  }

}
