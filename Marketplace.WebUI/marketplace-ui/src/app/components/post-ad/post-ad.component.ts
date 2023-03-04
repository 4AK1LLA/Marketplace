import { Component, OnInit } from '@angular/core';
import { CategoryDto, MainCategoryPostDto } from 'src/app/dto/main-category.dto';
import { TagDto } from 'src/app/dto/tag.dto';
import { MainCategoriesService } from 'src/app/services/main-categories-service/main-categories.service';
import { TagService } from 'src/app/services/tag-service/tag.service';

@Component({
  selector: 'app-post-ad',
  templateUrl: './post-ad.component.html',
  styleUrls: ['./post-ad.component.scss']
})
export class PostAdComponent implements OnInit {

  mainCategories: MainCategoryPostDto[] = [];

  category!: CategoryDto;
  tags: TagDto[] = [];

  constructor(private mainCategoriesService: MainCategoriesService, private tagService: TagService) { }

  ngOnInit(): void {
    this.mainCategoriesService.getAllForPosting().subscribe(mcList => this.mainCategories = mcList);
  }

  onCategoryClick(category: CategoryDto) { 
    this.category = category;

    this.tagService.getTagsByCategory(this.category.id).subscribe(tagList => this.tags = tagList);
  }
}