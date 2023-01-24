import { Component, Input } from '@angular/core';
import { MainCategoryDto } from 'src/app/dto/main-category.dto';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {
  @Input() mainCategories!: MainCategoryDto[];
}