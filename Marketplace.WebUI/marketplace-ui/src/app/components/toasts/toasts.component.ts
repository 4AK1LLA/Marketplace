import { Component, OnInit } from '@angular/core';
import { AppToastService } from 'src/app/services/toast-service/toast.service';

@Component({
  selector: 'app-toasts',
  template: `
  <ngb-toast
    *ngFor="let toast of toastService.toasts"
    [header]="toast.header" [autohide]="true" [delay]="toast.delay || 8000"
    (hiddden)="toastService.remove(toast)"
  >{{toast.body}}</ngb-toast>
  `,
  styles: [':host {position:fixed;bottom:0;right:0;margin:0.5em;z-index:1200;}']
})
export class ToastsComponent implements OnInit {

  constructor(public toastService: AppToastService) {}

  ngOnInit(): void {
  }

}
