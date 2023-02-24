import { Component } from '@angular/core';
import { MainCategoryDto } from './dto/main-category.dto';
import { EventTypes, OidcSecurityService, PublicEventsService } from 'angular-auth-oidc-client';
import { combineLatest, filter, take } from 'rxjs';
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
  isAuthenticated: boolean = false;

  constructor(
    private userService: UserService,
    private oidcSecurityService: OidcSecurityService,
    private eventService: PublicEventsService
  ) {
    this.checkSessionAndTryAuth();
    this.userContext = new UserContext;

    this.oidcSecurityService.checkAuth().subscribe(authResponse => {
      console.warn(authResponse);
    });

    console.log('isAuthenticated', this.isAuthenticated);
  }

  checkSessionAndTryAuth(): void {
    let eventService$ = this.eventService
      .registerForEvents()
      .pipe(filter((notification) =>
        notification.type === EventTypes.CheckingAuthFinishedWithError ||
        notification.type === EventTypes.CheckingAuthFinished ||
        notification.type === EventTypes.NewAuthenticationResult
      ), take(1));

    eventService$
      .subscribe(value => {
        if (value.type === EventTypes.CheckingAuthFinishedWithError) {
          this.isAuthenticated = false;
        }
        if (value.type === EventTypes.NewAuthenticationResult) {
          this.isAuthenticated = true;
        }
        if (value.type === EventTypes.CheckingAuthFinished) {
          this.oidcSecurityService.authorize(undefined, { customParams: { prompt: 'none', 'response-type': 'none', 'scope': 'openid' } });
        }
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