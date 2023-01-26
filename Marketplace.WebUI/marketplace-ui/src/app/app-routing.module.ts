import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainCategoriesComponent } from './components/main-categories/main-categories.component';
import { ProductsComponent } from './components/products/products.component';

const routes: Routes = [
  { path: ':mainCategoryId/:categoryId', component: ProductsComponent },
  { path: ':mainCategoryId', component: ProductsComponent },
  { path: '', component: MainCategoriesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }