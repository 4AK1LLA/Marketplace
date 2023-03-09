import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MainCategoriesComponent } from './components/main-categories/main-categories.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { AppRoutingModule } from './modules/app-routing.module';
import { ProductsComponent } from './components/products/products.component';
import { FooterComponent } from './components/footer/footer.component';
import { AuthConfigModule } from './modules/auth-config.module';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { PostAdComponent } from './components/post-ad/post-ad.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastsComponent } from './components/toasts/toasts.component';
import { FavouritesComponent } from './components/favourites/favourites.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    MainCategoriesComponent,
    SidebarComponent,
    ProductsComponent,
    FooterComponent,
    ProductDetailsComponent,
    PostAdComponent,
    UnauthorizedComponent,
    ToastsComponent,
    FavouritesComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AuthConfigModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
