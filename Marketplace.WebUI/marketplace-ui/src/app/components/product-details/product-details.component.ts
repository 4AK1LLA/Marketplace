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

  public product!: ProductDetailsDto;
  public priceOrSalary!: string;
  public isFound: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private service: ProductsService
  ) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      let productId = Number(params['productId']);

      if (isNaN(productId)) {
        this.isFound = false;
        return;
      }

      this.service.getProductDetails(productId)
        .subscribe(response => {
          if (response === null) {
            this.isFound = false;
            return;
          }

          this.product = response;
          this.initTagValuesAndPrice();
          this.isFound = true;
        });
    });
  }

  private initTagValuesAndPrice(): void {
    if (this.product !== null || this.product !== undefined) {
      let priceTag = this.product.tagValues.find(tv => tv.name === 'Price' || tv.name === 'Salary')!;
      let index = this.product.tagValues.indexOf(priceTag);
      this.priceOrSalary = priceTag.value!;

      this.product.tagValues.splice(index, 1);

      this.product?.tagValues.forEach(tv => {
        if (tv.name.toLowerCase().includes('area')) {
          tv.value += ' m<sup>2</sup>';
        }
        if (tv.name.toLowerCase().includes('experience')) {
          if (isNaN(Number(tv.value))) {
            tv.value += ' years';
            return;
          }
          switch (Number(tv.value)) {
            case 0: tv.value = 'Not required'; break;
            case 1: tv.value += ' year'; break;
            default: tv.value += ' years'; break;
          }
        }
      });
    }
  }
}
