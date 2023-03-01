import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-post-ad',
  templateUrl: './post-ad.component.html',
  styleUrls: ['./post-ad.component.scss']
})
export class PostAdComponent implements OnInit {

  category!: string;

  constructor() { }

  ngOnInit(): void {
  }

  onCategoryClick(category: string) {
    this.category = category;
  }
}
