import { Component, OnInit } from "@angular/core";
import { FaqServiceService } from 'src/app/_services/faq-service.service';
import { Faq } from 'src/app/_models/Faq';

@Component({
  selector: "app-services",
  templateUrl: "./services.component.html",
  styleUrls: ["./services.component.sass"]
})
export class ServicesComponent implements OnInit {

  faqs: Faq[]
  constructor(private faqService: FaqServiceService) { }

  ngOnInit() {
    this.faqService.getFaqs().subscribe((result: any) => {
      this.faqs = result
    })
  }
}
