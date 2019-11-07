import { Injectable } from '@angular/core';

// disable ts lint from throwing exceptions
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  confirm(message: string, OkCallback: () => any ) {
    // tslint:disable-next-line: only-arrow-functions
    alertify.confirm(message, function(e) {
      if (e) {
        OkCallback();
      } else {}
    });
  }

  success(message: string) {
    alertify.success(message);
  }

  error(message: string) {
    alertify.error(message);
  }

  warning(message: string) {
    alertify.warning(message);
  }

  message(message: string) {
    alertify.message(message);
  }
}
