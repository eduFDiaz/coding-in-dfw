import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/_models/Product';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.sass']
})
export class ContainerComponent implements OnInit {

  @Input() products: Product[]

  constructor() { }

  ngOnInit() {
  }

}
