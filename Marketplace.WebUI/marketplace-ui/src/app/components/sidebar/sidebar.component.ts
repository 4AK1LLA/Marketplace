import { Component, OnInit } from '@angular/core';
import { MainCategoryDto } from 'src/app/dto/main-category.dto';
import { MainCategoriesService } from 'src/app/services/main-categories-service/main-categories.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  mainCategories: MainCategoryDto[] = [];

  constructor(private categoriesService: MainCategoriesService) { }

  ngOnInit() {
    this.categoriesService.getAll().subscribe(data => this.mainCategories = data);
  }
}