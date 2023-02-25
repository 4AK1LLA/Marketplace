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

  public product?: ProductDetailsDto;

  constructor(
    private route: ActivatedRoute,
    private service: ProductsService
  ) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      let productId = Number(params['productId']);

      if (isNaN(productId)) {
        this.product = undefined;
        return;
      }

      this.service.getProductDetails(productId)
        .subscribe(response => {
          this.product = response;
        });
    });
  }

}
