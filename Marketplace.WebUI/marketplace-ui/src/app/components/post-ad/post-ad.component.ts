import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { CategoryDto, MainCategoryPostDto } from 'src/app/dto/main-category.dto';
import { TagDto } from 'src/app/dto/tag.dto';
import { BasicInfo, TagValue } from 'src/app/models/models';
import { MainCategoriesService } from 'src/app/services/main-categories-service/main-categories.service';
import { ProductsService } from 'src/app/services/products-service/products.service';
import { TagService } from 'src/app/services/tag-service/tag.service';
import { ToastService } from 'src/app/services/toast-service/toast.service';

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
    private tagService: TagService,
    private productService: ProductsService,
    private oidcSecurityService: OidcSecurityService,
    private toastService: ToastService,
    private router: Router) { }

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
    let i = this.tags.findIndex(tag => tag.id === tagId);
    this.tags[i].displayValue = value;

    this.form.get(tagId.toString())?.setValue(value);
  }

  public onCategoryClick(category: CategoryDto) {
    this.category = category;

    this.initTagsThenFormGroup();
  }

  public onChooseButtonClick(ctrlId: any) {
    this.form.get(ctrlId.toString())?.markAsTouched();
  }

  public onSubmit() {
    if (this.form.invalid) {
      this.markAllInvalidAndScrollOnTop();

      return;
    }

    this.oidcSecurityService.getAccessToken().subscribe(accessToken => {
      if (accessToken) {
        let payload = this.form.getRawValue();
        this.sendToApi(accessToken, payload);
      }
    });
  }

  private sendToApi(accessToken: string, payload: any) {
    let basicInfo: BasicInfo = {
      title: payload['title'],
      description: payload['description'],
      location: payload['location'],
      categoryId: payload['categoryId']
    };

    let tagValues: TagValue[] = [];

    for (let prop in payload) {
      if (!['title', 'description', 'location', 'categoryId'].includes(prop) && payload[prop].length > 0) {

        if (typeof payload[prop] !== 'string') {
          let arr = payload[prop] as string[];
          payload[prop] = '';
          arr.forEach(val => payload[prop] += (arr.indexOf(val) === arr.length - 1) ? val : `${val}|`);
        }

        tagValues.push({
          tagId: Number(prop),
          value: payload[prop]
        });
      }
    }

    this.productService.postProduct(accessToken, basicInfo, tagValues).subscribe(_ => {
      this.toastService.show('Notification', 'Your ad has been successfully published');
      this.router.navigate(['/']);
    });
  }

  private markAllInvalidAndScrollOnTop() {
    this.form.markAllAsTouched();
    for (let ctrlId in this.form.controls) {
      if (this.form.get(ctrlId)?.invalid) {
        let element = document.querySelector(`#id${ctrlId}`);
        element?.classList.add('is-invalid');
      }
    }
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}