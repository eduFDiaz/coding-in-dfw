import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/_models/Product';

@Component({
  selector: 'app-projects-list',
  templateUrl: './projects-list.component.html',
  styleUrls: ['./projects-list.component.sass']
})
export class ProjectsListComponent implements OnInit {

  @Input() projects: Product[]

  constructor() { }

  ngOnInit() {
  }

  stripHtml(html: string) {
    var div = document.createElement("DIV");
    div.innerHTML = html;
    let cleanText = div.innerText;
    div = null; // prevent mem leaks
    return cleanText;
  }

}
