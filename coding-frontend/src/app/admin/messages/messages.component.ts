import { Component, OnInit, TemplateRef } from '@angular/core';
import { MessagesService } from '../../_services/messages.service'
import { Message } from '../../_models/Message'

import { NbDialogService } from '@nebular/theme'

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.sass']

})

export class MessagesComponent implements OnInit {

  constructor(private messageService: MessagesService, private dialog: NbDialogService) { }

  messages: Message[]

  ngOnInit() {
  	this.messageService.getMessages().subscribe((result) => {
  		this.messages = result
  		console.log(this.messages)
  	})
  }

  openDeleteDialog(dialog: TemplateRef<any>, id: string) {
  	this.dialog.open(dialog).onClose.subscribe((result) => {
  		this.messageService.deleteMessage(id).subscribe((result) => {
  			console.log("OK borrado")
  		})
  	} )
  }

}
