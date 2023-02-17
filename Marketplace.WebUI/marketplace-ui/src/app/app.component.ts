import { Component } from '@angular/core';
import { MainCategoryDto } from './dto/main-category.dto';
import { MainCategoriesService } from './services/main-categories-service/main-categories.service';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  mainCategories: MainCategoryDto[] = [];

  constructor(
    private service: MainCategoriesService,
    private oidcSecurityService: OidcSecurityService
  ) {

    this.initMainCategories();

    //identity_cookie is IS cookie that used for telling IS authentication was proceeded earlier
    //OidcSecurityService stores auth data in session storage (when closing site it disappears)

    //The method checkAuth() is needed to process the redirect from your Security Token Service and set the correct states. This method must be used to ensure the correct functioning of the library. (from docs)
    this.oidcSecurityService.checkAuth().subscribe(authResponse =>
      console.warn(authResponse));
  }

  initMainCategories = () =>
    this.service
      .getAll()
      .subscribe(data => this.mainCategories = data);

  login() {
    this.oidcSecurityService.authorize();
  }

  access() {
    this.oidcSecurityService.getAccessToken().subscribe(token => console.log(token));
  }

  id() {
    this.oidcSecurityService.getIdToken().subscribe(id => console.log(id));
  }
}