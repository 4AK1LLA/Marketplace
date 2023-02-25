import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDetailsDto } from 'src/app/dto/product-details.dto';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  productDetailDto!: ProductDetailsDto;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let productId = Number(params['productId']);

      if (Number.isNaN(productId)) {
        //wrong param
      }

    });
  }

}
