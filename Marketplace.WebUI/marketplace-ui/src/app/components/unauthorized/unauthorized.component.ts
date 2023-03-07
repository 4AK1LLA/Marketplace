import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-unauthorized',
  template: `
  <div class="d-flex justify-content-center">
    <div class="spinner-border" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
  </div>
  `,
  styles: ['']
})
export class UnauthorizedComponent implements OnInit {

  constructor(private oidcSecurityService: OidcSecurityService) { }

  ngOnInit(): void {
    setTimeout(() => {
      this.oidcSecurityService.authorize();
    }, 5000);
  }


}
