import { Component, OnInit } from '@angular/core';
import { MainCategoryDto } from 'src/app/dto/main-category.dto';

@Component({
  selector: 'app-main-categories',
  templateUrl: './main-categories.component.html',
  styleUrls: ['./main-categories.component.css']
})
export class MainCategoriesComponent implements OnInit {

  mainCategories: MainCategoryDto[] = [];

  ngOnInit(): void {
    this.mainCategories = [ 
      { id: 1, name: 'Help', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/donate-heart-solid-96_n70lpq.png' },
      { id: 2, name: 'For children', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/baby-carriage-solid-96_nbpuhk.png' },
      { id: 3, name: 'Realty', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/key-solid-96_hftmhp.png' },
      { id: 4, name: 'Transport', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/car-solid-96_tzqygl.png' },
      { id: 5, name: 'Spare parts for transport', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242955/main-categories/wrench-solid-96_btyqrm.png' },
      { id: 6, name: 'Job', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/briefcase-solid-96_fhc7se.png' },
      { id: 7, name: 'Animals', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/cat-solid-96_i35w4q.png' },
      { id: 8, name: 'House and garden', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/building-house-solid-96_ucduvn.png' },
      { id: 9, name: 'Electronics', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674239930/main-categories/tv-solid-96_1_pifkzw.png' },
      { id: 10, name: 'Business and services', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/dollar-circle-solid-96_trmr52.png' },
      { id: 11, name: 'Fashion and style', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/t-shirt-solid-96_y3kgrq.png' },
      { id: 12, name: 'Hobby, rest and sport', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/football-regular-96_wgwuwe.png' },
      { id: 13, name: 'For free', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/purchase-tag-solid-96_e0pi0g.png' },
      { id: 14, name: 'Exchange', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/collapse-horizontal-regular-96_kfdrld.png' },
      { id: 15, name: 'Generators', photoUrl: 'https://res.cloudinary.com/dfs6whqzs/image/upload/v1674242819/main-categories/bulb-solid-96_fsawfj.png' }
    ];
  }
}
