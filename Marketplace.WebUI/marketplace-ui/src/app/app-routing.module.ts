import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainCategoriesComponent } from './components/main-categories/main-categories.component';

const routes: Routes = [
  { path: '', component: MainCategoriesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }