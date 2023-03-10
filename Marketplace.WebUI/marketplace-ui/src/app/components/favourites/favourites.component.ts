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
          this.likedProducts = likedProducts;
          this.activateLikes();
        });
    });
  }

  public onLikeClick(productId: number) {
    this.accessToken$.subscribe(accessToken => {
      this.productsService.likeProduct(accessToken, productId).subscribe(isLiked => {
        this.likedProducts.find(pr => pr.id === productId)!.liked = isLiked;
      });
    });
  }

  public onRemoveAllClick() {
    let result = confirm('Are you sure you want to remove all likes?');

    if (result) {
      this.accessToken$.subscribe(accessToken => {
        this.productsService.removeAllLikes(accessToken).subscribe(success => {
          if (!success) {
            return;
          }

          this.likedProducts = [];
        });
      });
    }
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
