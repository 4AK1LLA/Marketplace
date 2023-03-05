import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

  form: FormGroup = new FormGroup({});

  mainCategories: MainCategoryPostDto[] = [];
  category!: CategoryDto;
  tags: TagDto[] = [];

  constructor(private mainCategoriesService: MainCategoriesService, private tagService: TagService) { }

  ngOnInit(): void {
    this.mainCategoriesService.getAllForPosting().subscribe(mcList => this.mainCategories = mcList);
  }

  private initTags() {
    this.tagService.getTagsByCategory(this.category.id).subscribe(tagList => {
      tagList.forEach(tag => tag.value='');
      this.tags = tagList;
      this.form = this.toFormGroup(this.tags);
    });
  }

  public onDropdownClick(value: string, tagId: number) {
    //Displaying
    let i = this.tags.findIndex(tag => tag.id === tagId);
    this.tags[i].value = value;

    //Form value
    this.form.get(tagId.toString())?.setValue(value);
  }

  public onCategoryClick(category: CategoryDto) { 
    this.category = category;

    this.initTags();
  }

  public onSubmit() {
    console.log(JSON.stringify(this.form.getRawValue()));
  }

  private toFormGroup(tags: TagDto[] | null) {
    let group: any = {};

    tags!.forEach(tag => {
      if (tag.type==='dropdown') {
        group[tag.id] = tag.isRequired ? new FormControl('', Validators.required)
        : new FormControl('');
      }
    });

    return new FormGroup(group);
  }
}