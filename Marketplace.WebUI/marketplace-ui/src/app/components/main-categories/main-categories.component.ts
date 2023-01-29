import { Component, OnInit } from '@angular/core';
import { MainCategoryDto } from 'src/app/dto/main-category.dto';
import { MainCategoriesService } from 'src/app/services/main-categories-service/main-categories.service';

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

  onClick(route: string): void {
    this.mainCategories.forEach(item => {
      document.getElementById(item.route)?.classList.remove('show');
      document.getElementById(item.route + 'button')?.classList.add('collapsed');
    });

    let collapsedClasses = document.getElementById(route)!.classList;
    let buttonClasses = document.getElementById(route + 'button')!.classList;

    collapsedClasses.add('show');
    buttonClasses.remove('collapsed');

    document.getElementById('offcanvasId')?.addEventListener('hidden.bs.offcanvas', () => {
      collapsedClasses.remove('show');
      buttonClasses.add('collapsed');
    });
  }
}