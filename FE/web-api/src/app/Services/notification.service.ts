import { Injectable } from '@angular/core';
import {  ToastrService } from 'ngx-toastr';
import $ from "jquery";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private toastr: ToastrService) { }
  ShowMessage(type: "success" | "error" | "info" | "warning", message: string, tile?: string,custom?: object) {

    switch (type) {
      case "success":
        this.toastr.success(message, tile,custom);
        break;
      case "error":
        this.toastr.error(message, tile,custom);
        break;
      case "info":
        this.toastr.info(message, tile,custom);
        break;
      case "warning":
        this.toastr.warning(message, tile,custom);
        break;
      default:
        this.toastr.show(message, tile,custom);
        break;
    }
  }
  showCenterLoading(){
    if(window.parent){
      $(window.parent.document).find('.center-loading').show();
    }
    else{
      $('.center-loading').show();
    }
  }
  hideCenterLoading(){
    if(window.parent){
      $(window.parent.document).find('.center-loading').hide();
    }
    else{
      $('.center-loading').hide();
    }

  }
}
