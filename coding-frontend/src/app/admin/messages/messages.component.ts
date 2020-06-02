import { Component, OnInit, TemplateRef } from '@angular/core';
import { MessagesService } from '../../_services/messages.service'
import { Message } from '../../_models/Message'

import { NbDialogService } from '@nebular/theme'

import { AlertService } from '../../_services/alert.service'

@Component({
	selector: 'app-messages',
	templateUrl: './messages.component.html',
	styleUrls: ['./messages.component.sass']

})

export class MessagesComponent implements OnInit {

	constructor(
		private alert: AlertService,
		private messageService: MessagesService, private dialog: NbDialogService) { }

	messages: Message[]

	ngOnInit() {
		this.messageService.getMessages().subscribe((result) => {
			this.messages = result.filter((item: any) => item.isRead == false)
			console.log(this.messages)
		})
	}

	openDeleteDialog(dialog: TemplateRef<any>, id: string) {
		this.dialog.open(dialog).onClose.subscribe((result) => {
			this.messageService.deleteMessage(id).subscribe((result) => {
				this.messages = this.messages.filter((obj: any) => obj.id !== id)
				this.alert.showToast('top-right', 'info', 'Deleted', 'Your message was deleted!')
			})
		})
	}

	markRead(id: string) {
		this.messageService.markRead(id).subscribe((result) => {
			this.messages = this.messages.filter((obj: any) => obj.id !== id)
			this.alert.showToast('top-right', 'success', 'Ok', 'Mark read')

		})
	}

	viewAll() {
		this.messageService.getMessages().subscribe((result) => {
			this.messages = result
		})
	}

	viewUnread() {
		this.messageService.getMessages().subscribe((result) => {
			this.messages = result.filter((item: any) => item.isRead == false)
			console.log(this.messages)
		})
	}

}
