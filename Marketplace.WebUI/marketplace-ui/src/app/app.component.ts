import { Component } from '@angular/core';
import { MainCategoryDto } from './dto/main-category.dto';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { combineLatest } from 'rxjs';
import { UserService } from './services/user-service/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  mainCategories: MainCategoryDto[] = [];
  isAuthenticated: boolean = false;

  constructor(
    private userService: UserService,
    private oidcSecurityService: OidcSecurityService
  ) {

    //identity_cookie is IS cookie that used for telling IS authentication was proceeded earlier
    //OidcSecurityService stores auth data in session storage (when closing site it disappears)

    //The method checkAuth() is needed to process the redirect from your Security Token Service and set the correct states. This method must be used to ensure the correct functioning of the library. (from docs)
    this.oidcSecurityService.checkAuth().subscribe(authResponse => {
      this.isAuthenticated = authResponse.isAuthenticated;
      console.warn(authResponse);
      console.log(authResponse.accessToken)
    });
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  createUser() {
    let params$ = combineLatest({
      user: this.oidcSecurityService.userData$,
      accessToken: this.oidcSecurityService.getAccessToken()
    });

    params$.subscribe(params => {
      console.log(`email : ${params.user.userData.name} \ntoken : ${params.accessToken}`)
    });

    this.userService.createUser().subscribe(response => console.log(response));
  }
}