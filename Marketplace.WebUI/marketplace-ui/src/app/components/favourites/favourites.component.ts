import { Component, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/dto/product.dto';

@Component({
  selector: 'app-favourites',
  templateUrl: './favourites.component.html',
  styleUrls: ['./favourites.component.scss']
})
export class FavouritesComponent implements OnInit {

  likedProducts: ProductDto[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
