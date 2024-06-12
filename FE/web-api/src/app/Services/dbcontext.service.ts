import { Injectable } from '@angular/core';
import { NotificationService } from './notification.service';
import { BaseApiService } from './base-api.service';

@Injectable({
  providedIn: 'root'
})
export class DbcontextService {

  constructor(private notification: NotificationService,private api: BaseApiService,) { }
  GetObject(link: string, model?: any) {
    this.notification.showCenterLoading();
    let _this = this;
    return new Promise<any>((resolve, reject) => {
      _this.api.get(link).subscribe(data => {
        if (!data) {
          resolve({});
         _this.notification.hideCenterLoading();
          return;
        }
        resolve(data);
        _this.notification.hideCenterLoading();
      },
        err => {
          resolve({});
         _this.notification.hideCenterLoading();
        });
    });
  }
  PostObject(link: string, model?: any) {
    //this.notification.showCenterLoading();
    let _this = this;
    return new Promise<any>((resolve, reject) => {
      _this.api.post(link,model).subscribe(data => {
        if (!data) {
          resolve({});
         _this.notification.hideCenterLoading();
          return;
        }
        resolve(data);
        _this.notification.hideCenterLoading();
      },
        err => {
          resolve({});
         _this.notification.hideCenterLoading();
        });
    });
  }
}
