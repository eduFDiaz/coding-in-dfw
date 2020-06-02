import { Component, OnInit, ViewChild } from '@angular/core';
import { Message } from '../../_models/Message'
import { MessagesService } from '../../_services/messages.service'
import { AlertifyServiceService } from 'src/app/_services/alertify-service.service';

import { NgForm} from '@angular/forms'

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.sass']
})
export class ContactComponent implements OnInit {

  @ViewChild('contactForm', { static: false }) contactForm: NgForm;

  constructor(private messageService: MessagesService, private alert: AlertifyServiceService) { }

  userData: any

  ngOnInit() {
    this.userData = JSON.parse(localStorage.getItem('userdata'))
  }

  newmessage = {
  	name: '',
  	email: '',
  	serviceType: '',
  	text: ''
  }

  newMessage(message: Message) {
  	console.log(message)
  	this.messageService.createMessage(message).subscribe((result) => {
  		this.alert.success('Thanks for your message, ill contact you back as soon as possible.')
  		this.contactForm.resetForm()
  	}, error => {
  		this.alert.error('Cant upload your message right now , sorry :( ')
  	})
  }

}
