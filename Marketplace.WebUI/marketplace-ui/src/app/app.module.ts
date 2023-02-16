import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MainCategoriesComponent } from './components/main-categories/main-categories.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { AppRoutingModule } from './app-routing.module';
import { ProductsComponent } from './components/products/products.component';
import { FooterComponent } from './components/footer/footer.component';
import { AuthModule } from 'angular-auth-oidc-client';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MainCategoriesComponent,
    SidebarComponent,
    ProductsComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AuthModule.forRoot(
      {
        config: {
          configId: 'identity_server',
          authority: 'https://localhost:7028',
          redirectUrl: window.location.origin,
          postLogoutRedirectUri: window.location.origin,
          clientId: 'angular_ui',
          scope: 'openid',
          responseType: 'code',
          silentRenew: false,
          useRefreshToken: false,
        }
      }
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
