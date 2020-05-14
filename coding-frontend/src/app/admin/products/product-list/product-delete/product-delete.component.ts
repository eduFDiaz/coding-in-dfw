import { Component, OnInit, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';
import { catchError } from 'rxjs/operators';
import { UserService } from 'src/app/_services/user.service';
import { ProductService } from 'src/app/_services/product.service';
import { AlertService } from 'src/app/_services/alert.service';


@Component({
  selector: 'app-product-delete',
  templateUrl: './product-delete.component.html',
  styleUrls: ['./product-delete.component.scss']
})
export class ProductDeleteComponent implements OnInit {

  @Input() product: any

  deleteSpinner = false;

  constructor(private user: UserService, protected dialogRef: NbDialogRef<any>, private productService: ProductService, private toast: AlertService) { }

  ngOnInit() {

  }

  close(status: any) {
    this.dialogRef.close(status);
  }

  deleteItem(id: number) {
    this.deleteSpinner = true
    this.productService.deleteProduct(id).subscribe(result => {
      this.deleteSpinner = false
      this.toast.showToast('bottom-left', 'info', 'Delete ok', 'Your product has been deleted!')
      this.productService.getProducts(this.user.getCurrentUserId()).subscribe((data) => {
        let postslenght
        postslenght = data.length
        this.dialogRef.close(postslenght)
      }, err => {
        const myerr = err
        this.dialogRef.close(myerr)
      })

    })

  }

}
