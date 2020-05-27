import { Component, OnInit, Input, ViewChild, TemplateRef } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { UserService } from 'src/app/_services/user.service';
import { ProductService } from 'src/app/_services/product.service';
import { AlertService } from 'src/app/_services/alert.service';
import { Product } from 'src/app/_models/Product';
import { Requirement } from 'src/app/_models/Requirement';


@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {

  @ViewChild('editButton', { static: true }) editButton: TemplateRef<any>

  editSpinner = false

  @Input() product: any

  requirements: Requirement[]

  deleteSpinner = false;

  constructor(
    private alert: AlertService,
    private user: UserService,
    protected dialogRef: NbDialogRef<any>,
    private productService: ProductService,
    private toast: AlertService) { }

  ngOnInit() {
    this.requirements = this.product.productRequirements
    console.log(this.requirements)
  }

  editItem(id: string, product: Product) {
    this.editSpinner = true
    product.requirementId = this.requirements.filter((item) => {
      return item.id
    })
    console.log(product.requirementId)
    this.productService.editProduct(id, product).subscribe(result => {
      this.editSpinner = false
      this.toast.showToast('bottom-left', 'info', 'Update ok', 'Your product has been updated!')
      this.productService.getProducts(this.user.getCurrentUserId()).subscribe((data) => {
        let productlenght
        productlenght = data.length
        this.dialogRef.close(productlenght)
      }, err => {
        const myerr = err
        this.dialogRef.close(myerr)
      })
      this.dialogRef.close()

    })

  }

  removeRequirement(id: string) {
    this.productService.deleteRequirement(id).subscribe(result => {
      this.alert.showToast('top-right', 'info', 'Deleted', 'Requirement Deleted')
      this.requirements = this.requirements.filter((item) => {
        item.id !== id

      })

    })
  }
}
