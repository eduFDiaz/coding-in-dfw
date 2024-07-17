import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { NbDialogService } from '@nebular/theme'
import { ProductDeleteComponent } from './product-delete/product-delete.component';

import { Router } from '@angular/router';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { Product } from 'src/app/_models/Product';
import { AlertService } from 'src/app/_services/alert.service';
import { ProductService } from 'src/app/_services/product.service';
import { UserService } from 'src/app/_services/user.service';


@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  @ViewChild('dialogDelete', { static: true }) dialogRef: TemplateRef<any>;

  products: Product[]

  spinner = false;


  constructor(private route: Router, private toast: AlertService, private dialog: NbDialogService, private productService: ProductService, private user: UserService) { }

  ngOnInit() {
    this.spinner = true
    this.productService.getProducts(this.user.getCurrentUserId()).subscribe((data) => {
      if (data.length > 0) {
        this.products = data

        this.spinner = false
      } else {
        this.toast.showToast('top-right', 'info', 'Theres no products here :(', 'Cant find any product')
      }
    }, error => {

      this.spinner = false
      this.toast.showToast('top-right', 'info', 'Theres no products here :(', 'Cant find any product')
    })
  }

  goToAddProduct() {
    this.route.navigate(['product/new'])
  }

  // Using dialog in a component
  openDeleteDialog(productToDelete: Object) {
    this.dialog.open(ProductDeleteComponent, {
      context: {
        product: productToDelete
      }, closeOnBackdropClick: false
    }).onClose.subscribe((data) => {
      this.spinner = true;
      if (data) {
        if (data.status === 404) {
          this.spinner = false
          this.products = []
          this.toast.showToast('top-right', 'info', 'Theres no products here :(', 'Cant find any product')
        } else {
          this.productService.updatedProduct.subscribe((result) => {
            if (result) {
              this.products = result
              this.spinner = false;
            }
          })
        }
        if (data === 'closed') {
          this.productService.getProducts(this.user.getCurrentUserId()).subscribe((result) => {
            this.products = result
          }
          )
        }
      }
    });
  }

  openEditDialog(productToEdit: Product) {
    this.route.navigate(['product/edit'], {
      queryParams: {
        forproduct: productToEdit.id
      }
    })

  }

}



