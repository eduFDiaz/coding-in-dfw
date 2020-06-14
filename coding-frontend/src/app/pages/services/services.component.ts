import { Component, OnInit } from "@angular/core";
import { FaqServiceService } from 'src/app/_services/faq-service.service';
import { Faq } from 'src/app/_models/Faq';
import { User } from 'src/app/_models/User';

@Component({
  selector: "app-services",
  templateUrl: "./services.component.html",
  styleUrls: ["./services.component.sass"]
})
export class ServicesComponent implements OnInit {

  faqs: Faq[]
  user: User
  constructor(private faqService: FaqServiceService) { }

  ngOnInit() {
    this.faqService.getFaqs().subscribe((result: any) => {
      this.faqs = result
    })
    this.user = JSON.parse(localStorage.getItem('userdata'))
  }

  stripHtml(html: string) {
    var div = document.createElement("P");
    div.innerHTML = html;
    let cleanText = div.innerText;
    div = null; // prevent mem leaks
    return cleanText;
  }
}
