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

  constructor(private service: MainCategoriesService, public oidcSecurityService: OidcSecurityService) {
    this.initMainCategories();

    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated, userData, accessToken, idToken }) => {
      console.log(userData)
    });
  }

  initMainCategories = () =>
    this.service
      .getAll()
      .subscribe(data => this.mainCategories = data);

  login() {
    this.oidcSecurityService.authorize();
  }
}