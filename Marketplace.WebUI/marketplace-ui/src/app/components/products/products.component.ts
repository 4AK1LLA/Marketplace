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
    
  }

}
