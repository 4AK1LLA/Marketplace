import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { ProductDto } from 'src/app/dto/product.dto';
import { ProductsService } from 'src/app/services/products-service/products.service';

@Component({
  selector: 'app-favourites',
  templateUrl: './favourites.component.html',
  styleUrls: ['./favourites.component.scss']
})
export class FavouritesComponent implements OnInit {

  likedProducts!: ProductDto[];

  accessToken$ = this.oidcSecurityService.getAccessToken();

  constructor(
    private productsService: ProductsService,
    private oidcSecurityService: OidcSecurityService
  ) { }

  ngOnInit(): void {
    this.accessToken$.subscribe(accessToken => {
      if (!accessToken) {
        return;
      }

      this.productsService
        .getLikedProducts(accessToken)
        .subscribe(likedProducts => {
          console.log(likedProducts)
          this.likedProducts = likedProducts;
          this.activateLikes();
        });
    });
  }

  private activateLikes() {
    if (!this.likedProducts) {
      return;
    }

    this.likedProducts.forEach(product => {
      product.liked = true;
    });
  }
}
