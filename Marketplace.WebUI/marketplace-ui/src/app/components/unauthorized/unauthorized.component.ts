import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-unauthorized',
  template: `
  <div class="d-flex justify-content-center align-items-center mb-3">
    <div class="spinner-border me-3" role="status">
      <span class="visually-hidden">Loading...</span>
    </div>
    <div><strong>Redirecting to authorization page</strong></div>
  </div>
  `,
  styles: ['.spinner-border { height:3rem; width:3rem;} .d-flex { padding-top:13rem; padding-bottom:10rem; }']
})
export class UnauthorizedComponent implements OnInit {

  constructor(private oidcSecurityService: OidcSecurityService) { }

  ngOnInit(): void {
    setTimeout(() => {
      this.oidcSecurityService.authorize();
    }, 1000);
  }


}
