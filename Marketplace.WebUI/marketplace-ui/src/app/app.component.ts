import { Component } from '@angular/core';
import { CategoryDto, MainCategoryDto } from './dto/main-category.dto';
import { MainCategoriesService } from './services/main-categories.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  mainCategories: MainCategoryDto[] = [];

  constructor(private service: MainCategoriesService) {
    this.initMainCategories();
  }

  initMainCategories = () =>
    this.service
      .getAll()
      .subscribe(data => { 
        this.mainCategories = data; 
        this.mainCategories[0].subCategories = [
          { id: 1, name: "Need help" },
          { id: 2, name: "Give help" }
        ];
      });
}