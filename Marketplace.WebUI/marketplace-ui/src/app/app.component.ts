import { Component } from '@angular/core';
import { MainCategoryDto } from './dto/main-category.dto';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { combineLatest } from 'rxjs';
import { UserService } from './services/user-service/user.service';
import { UserContext } from './contexts/user.context';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  mainCategories: MainCategoryDto[] = [];
  userContext: UserContext;

  constructor(
    private userService: UserService,
    private oidcSecurityService: OidcSecurityService
  ) {
    this.userContext = new UserContext;

    //identity_cookie is IS cookie that used for telling IS authentication was proceeded earlier
    //OidcSecurityService stores auth data in session storage (when closing site it disappears)

    //The method checkAuth() is needed to process the redirect from your Security Token Service and set the correct states. This method must be used to ensure the correct functioning of the library. (from docs)
    this.oidcSecurityService.checkAuth().subscribe(authResponse => {
      console.warn(authResponse);
      this.userContext.isAuthenticated = authResponse.isAuthenticated;
      this.userContext.name = (authResponse.userData != null) ? authResponse.userData.name : null;
    });
  }

  login = () => this.oidcSecurityService.authorize();
  logout = () => this.oidcSecurityService.logoffAndRevokeTokens().subscribe();

  createUser() {
    let params$ = combineLatest({
      user: this.oidcSecurityService.userData$,
      accessToken: this.oidcSecurityService.getAccessToken()
    });

    params$.subscribe(params => {
      let email = params.user.userData.name;
      let token = params.accessToken;
            
      this.userService
        .createUser(email, token)
        .subscribe(response => console.log(response));
    });
  }
}