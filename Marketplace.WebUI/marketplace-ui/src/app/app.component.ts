import { Component, OnInit } from '@angular/core';
//import { MainCategoryDto } from './dto/main-category.dto';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { combineLatest } from 'rxjs';
import { UserService } from './services/user-service/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  identityCookie: string = 'identity_cookie';
  //mainCategories: MainCategoryDto[] = [];
  isAuthenticated: boolean = false;

  constructor(
    private userService: UserService,
    private oidcSecurityService: OidcSecurityService
  ) { }

  public ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe(localAuth => {
      this.authorizeIfCookieExist(this.identityCookie, localAuth.isAuthenticated);
      this.isAuthenticated = localAuth.isAuthenticated;
      console.log(localAuth.userData)
    });
  }

  public onLogin = (): void => this.oidcSecurityService.authorize();
  public onLogout = () => this.oidcSecurityService.logoffAndRevokeTokens().subscribe();

  public onCreateUser(): void {
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

  private authorizeIfCookieExist(cookie: string, isAuth: boolean): void {
    document.cookie = cookie + '=0;path=/;';

    if (!isAuth && document.cookie.indexOf(cookie) === -1) { 
      this.oidcSecurityService.authorize();
    }
  }
}