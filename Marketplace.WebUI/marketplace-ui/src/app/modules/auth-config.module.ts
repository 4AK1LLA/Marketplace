import { NgModule } from '@angular/core';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';

@NgModule({
    imports: [AuthModule.forRoot({
        config: {
            configId: 'identity_server',
            clientId: 'angular_ui',
            authority: 'https://localhost:7028',
            redirectUrl: window.location.origin,
            postLogoutRedirectUri: window.location.origin,
            scope: 'openid profile email Marketplace.WebAPI details',
            responseType: 'code',
            silentRenew: false,
            useRefreshToken: false,
            autoUserInfo: false,
            logLevel: LogLevel.Debug
          }
      })],
    exports: [AuthModule],
})
export class AuthConfigModule { }
