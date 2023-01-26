import { Component, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/dto/product.dto';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  products: ProductDto[] = [];

  constructor() { }

  ngOnInit(): void {
    this.products = [
      { 
        id: 1, 
        title: "Table for work", 
        description: "Very good table", 
        publicationDate: new Date(), 
        location: "Kyiv", 
        tagValues: [ { value: "10000", name: "Price" }, { value: "Glass", name: "Material" } ], 
        photos:  [ { id: 1, isMain: false, url: "" } ]
      },
      { 
        id: 2, 
        title: "Children table for study", 
        description: "Table for study was bought in Epicenter", 
        publicationDate: new Date(), 
        location: "Lviv", 
        tagValues: [ { value: "3000", name: "Price" }, { value: "Wood", name: "Material" } ], 
        photos:  [ { id: 1, isMain: false, url: "" } ]
      }
    ]
  }

}
