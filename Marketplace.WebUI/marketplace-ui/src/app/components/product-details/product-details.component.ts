import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { ProductDetailsDto } from 'src/app/dto/product-details.dto';
import { ProductsService } from 'src/app/services/products-service/products.service';
import { ToastService } from 'src/app/services/toast-service/toast.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  public product!: ProductDetailsDto;
  public priceOrSalary!: string;
  public isFound: boolean = false;

  accessToken$ = this.oidcSecurityService.getAccessToken();

  constructor(
    private route: ActivatedRoute,
    private service: ProductsService,
    private oidcSecurityService: OidcSecurityService,
    private productsService: ProductsService,
    private toastService: ToastService
  ) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      let productId = Number(params['productId']);

      if (isNaN(productId)) {
        this.isFound = false;
        return;
      }

      this.accessToken$.subscribe(accessToken => {

        this.service.getProductDetails(productId, accessToken)
        .subscribe(response => {
          if (!response) {
            this.isFound = false;
            return;
          }

          this.product = response;
          this.initTagValuesAndPrice();
          this.isFound = true;
        });

      });
    });
  }

  public onLikeClick() {
    this.accessToken$.subscribe(accessToken => {
      if (!accessToken) {
        this.toastService.show('Notification', 'Sorry, you must be logged to save ads');
        return;
      }
      this.productsService.likeProduct(accessToken, this.product.id).subscribe(isLiked => {
        this.product.isLiked = isLiked;
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
