import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/_services/product.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/_models/Product';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.sass']
})
export class ProjectDetailComponent implements OnInit {

  constructor(private productService: ProductService, private route: ActivatedRoute) { }

  product: Product

  ngOnInit() {
    this.route.params.subscribe((data) => {
      this.productService.getProduct(data.id).subscribe((result: Product) => {
        this.product = result
        console.log(this.product)
      })
    })
  }

  stripHtml(html: string) {
    var div = document.createElement("P");
    div.innerHTML = html;
    let cleanText = div.innerText;
    div = null; // prevent mem leaks
    return cleanText;
  }

}
