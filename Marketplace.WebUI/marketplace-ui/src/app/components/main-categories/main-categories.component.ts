import { Component, Input } from '@angular/core';
import { MainCategoryDto } from 'src/app/dto/main-category.dto';

@Component({
  selector: 'app-main-categories',
  templateUrl: './main-categories.component.html',
  styleUrls: ['./main-categories.component.scss']
})
export class MainCategoriesComponent {
  @Input() mainCategories!: MainCategoryDto[];
}
