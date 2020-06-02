
import { Component, OnInit } from '@angular/core';
import { AlertService } from '../../_services/alert.service';
import { UserService } from 'src/app/_services/user.service';
import { Product } from 'src/app/_models/Product';
import { ProductService } from 'src/app/_services/product.service';
import { User } from 'src/app/_models/User';
import { Post } from 'src/app/_models/Post';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass']
})
export class HomeComponent implements OnInit {

  userData: any
  avatar: any
  products: Product[]
  userid: User
  posts: Post[]

  constructor(
    private postService: PostService,
    private productService: ProductService,
    private alert: AlertService, private user: UserService) { }

  ngOnInit() {
    this.user.getAllUsers().subscribe((userdata) => {
      this.userData = userdata[0]
      localStorage.setItem('userdata', JSON.stringify(this.userData))
    })
    this.user.getAllUserPhotos().subscribe((result: any) => {
      this.avatar = result.filter((item: any) => item.isMain == true)[0].url
    })
    this.userid = JSON.parse(localStorage.getItem('userdata'))
    this.productService.getProducts(this.userid.id).subscribe((result) => {
      this.products = result
      console.log(this.products)
    })
    this.postService.getUserPosts(this.userid.id).subscribe((result) => {
      this.posts = result
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
