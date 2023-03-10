import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizationGuard } from '../auth/authorization-guard';
import { FavouritesComponent } from '../components/favourites/favourites.component';
import { MainCategoriesComponent } from '../components/main-categories/main-categories.component';
import { PostAdComponent } from '../components/post-ad/post-ad.component';
import { ProductDetailsComponent } from '../components/product-details/product-details.component';
import { ProductsComponent } from '../components/products/products.component';
import { UnauthorizedComponent } from '../components/unauthorized/unauthorized.component';

const routes: Routes = [
  { path: 'favourites', component: FavouritesComponent, canActivate: [AuthorizationGuard] },
  { path: 'post-ad', component: PostAdComponent, canActivate: [AuthorizationGuard] },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: 'product/:productId', component: ProductDetailsComponent },
  { path: ':mainCategoryRoute/:categoryRoute', component: ProductsComponent },
  { path: ':mainCategoryRoute', component: ProductsComponent },
  { path: '', component: MainCategoriesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'enabled'})],
  exports: [RouterModule]
})
export class AppRoutingModule { }