import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDetailsDto } from 'src/app/dto/product-details.dto';
import { ProductsService } from 'src/app/services/products-service/products.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product!: ProductDetailsDto;

  constructor(private route: ActivatedRoute, private service: ProductsService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      let productId = Number(params['productId']);

      if (Number.isNaN(productId)) {
        //wrong param
      }

      this.service.getProductDetails(productId).subscribe(response => { this.product = response; console.log(response) });
    });
  }

}
