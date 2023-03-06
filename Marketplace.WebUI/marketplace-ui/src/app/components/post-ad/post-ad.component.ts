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

  mainCategories: MainCategoryPostDto[] = [];
  form!: FormGroup;
  category!: CategoryDto;
  tags!: TagDto[];

  constructor(
    private mainCategoriesService: MainCategoriesService, 
    private tagService: TagService) { }

  ngOnInit(): void {
    this.initFormGroup();
    this.mainCategoriesService.getAllForPosting().subscribe(mcList => this.mainCategories = mcList);
  }

  private initFormGroup() {
    this.form = new FormGroup({});

    let basicInfoControls = [
      { name: 'title', value: '' },
      { name: 'categoryId', value: (this.category) ? this.category.id : '' },
      { name: 'description', value: '' },
      { name: 'location', value: '' }
    ];

    basicInfoControls.forEach(control => {
      this.form.addControl(control.name, new FormControl(control.value, Validators.required));
    });

    if (!this.tags) {
      return;
    }
    
    this.tags.forEach(tag => {
      let value = (tag.type === 'checkbox') ? [] : (tag.type === 'switch') ? false : '';
      let control = new FormControl(value);

      if (tag.isRequired) {
        control.addValidators(Validators.required);
      }

      this.form.addControl(tag.id.toString(), control);
    });
  }

  private initTagsThenFormGroup() {
    this.tagService.getTagsByCategory(this.category.id).subscribe(tagList => {
      this.tags = tagList;

      if (this.tags) {
        this.tags.forEach(tag => tag.displayValue = '');
      }

      this.initFormGroup();
    });
  }

  public onDropdownClick(value: string, tagId: number) {
    let i = this.tags.findIndex(tag => tag.id === tagId);
    this.tags[i].displayValue = value;

    this.form.get(tagId.toString())?.setValue(value);
  }

  public onCheckBoxChange(target: any, value: string, tagId: number) {
    let current = this.form.get(tagId.toString())?.value as string[];

    if (!target.checked) {
      let i = current.indexOf(value);
      current.splice(i, 1);
    }

    if (target.checked) {
      current.push(value);
    }

    this.form.get(tagId.toString())?.setValue(current);
  }

  public onPillsOptionClick(value: string, tagId: number) {
    //Displaying
    let i = this.tags.findIndex(tag => tag.id === tagId);
    this.tags[i].displayValue = value;

    //Form value
    this.form.get(tagId.toString())?.setValue(value);
  }

  public onCategoryClick(category: CategoryDto) {
    this.category = category;

    this.initTagsThenFormGroup();
  }

  public onDropdownButtonClick(ctrlId: string) {
    this.form.get(ctrlId)?.markAsTouched();
  }

  public onSubmit() {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      for (let ctrlId in this.form.controls) {
        if (!this.form.get(ctrlId)?.valid) {
          let element = document.querySelector(`#${ctrlId}`);
          element?.classList.add('is-invalid');
        }
      }
    }

    console.log(this.form.valid);
    console.log(JSON.stringify(this.form.getRawValue()));
  }
}