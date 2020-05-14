import { Injectable } from '@angular/core';

import { NbToastrService } from '@nebular/theme';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  index = 1
  constructor(private toast: NbToastrService) { }

  showToast(position, status, title, subtitle) {
    this.toast.show(title +
      '',
      `` + subtitle,
      // `Error: ${++this.index}`,
      { position, status });
  }



}
