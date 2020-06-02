import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/_services/product.service';
import { Product } from 'src/app/_models/Product';
import { User } from 'src/app/_models/User';
import { ActivatedRoute } from '@angular/router';




@Component({
  selector: 'app-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.sass']
})
export class PortfolioComponent implements OnInit {

  active: boolean



  constructor(

    private activatedRouter: ActivatedRoute,
    private productService: ProductService) { }
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

  filterProducts(type: string) {
    switch (type) {
      case 'web':
        this.products = this.products.filter(item => item.type == 'web')
        break;

      case 'mobile':
        this.products = this.products.filter(item => item.type == 'mobile')
        break;

      case 'desktop':
        this.products = this.products.filter(item => item.type == 'desktop')
        break;

      default:
        this.productService.getProducts(this.currentUser.id).subscribe((prod) => {
          this.products = prod
          console.log(this.products)
        })
        break;
    }
  }

  stripHtml(html: string) {
    var div = document.createElement("DIV");
    div.innerHTML = html;
    let cleanText = div.innerText;
    div = null; // prevent mem leaks
    return cleanText;
  }

  getType() {
    let val
    this.activatedRouter.queryParams.subscribe((result) => {
      val = result['query']
    })

    return val
  }

  resolveClass(product: Product) {
    return product.type
  }


}
