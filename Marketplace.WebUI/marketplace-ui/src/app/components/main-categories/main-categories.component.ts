import { Component, OnInit } from '@angular/core';
import { MainCategoryDto } from 'src/app/dto/main-category.dto';
import { MainCategoriesService } from 'src/app/services/main-categories.service';

@Component({
  selector: 'app-main-categories',
  templateUrl: './main-categories.component.html',
  styleUrls: ['./main-categories.component.scss']
})
export class MainCategoriesComponent implements OnInit {
  mainCategories: MainCategoryDto[] = [];

  constructor(private categoriesService: MainCategoriesService) { }

  ngOnInit() {
    this.categoriesService.getAll().subscribe(data => this.mainCategories = data);
  }
}