import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/services/toast-service/toast.service';

@Component({
  selector: 'app-toasts',
  template: `
  <ngb-toast
    *ngFor="let toast of toastService.toasts" class="bg-info text-light"
    [header]="toast.header" [autohide]="true" [delay]="toast.delay || 8000"
    (hiddden)="toastService.remove(toast)"
  >{{toast.body}}</ngb-toast>
  `,
  styles: [':host {position:fixed;bottom:0;right:0;margin:0.5em;z-index:1200;}']
})
export class ToastsComponent implements OnInit {

  constructor(public toastService: ToastService) {}

  ngOnInit(): void {
  }

}
