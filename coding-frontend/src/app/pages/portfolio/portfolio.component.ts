import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/_services/product.service';
import { Product } from 'src/app/_models/Product';
import { User } from 'src/app/_models/User';

@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.sass']
})
export class PortfolioComponent implements OnInit {

  constructor(private productService: ProductService) { }
  currentUser: User

  products: Product[]

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem('userdata'))
    this.productService.getProducts(this.currentUser.id).subscribe((prod) => {
      this.products = prod
      console.log(this.products)
    })
  }

  // TODO: hacer una funcion que filtre todos los productos por su categoria, igualando el
  // resultado filtrado al mismo array de productos.



}
