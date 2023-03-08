import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { combineLatest, debounceTime } from 'rxjs';
import { ProductDto } from 'src/app/dto/product.dto';
import { PaginationService } from 'src/app/services/pagination-service/pagination.service';
import { ProductsService } from 'src/app/services/products-service/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  productsCount!: number;
  products: ProductDto[] = [];
  routeValue!: string;

  //pagination
  page!: number;
  pagesCount!: number;
  paginationArray: any[] = [];

  constructor(
    private productsService: ProductsService,
    private route: ActivatedRoute,
    private paginationService: PaginationService,
    private oidcSecurityService: OidcSecurityService
  ) { }

  ngOnInit(): void { //now Im using routerLink (removed hrefs) wich doesnt require page reloading, so this hook and observable subscription are executing during whole component lifetime
    let route$ = combineLatest({ qparams: this.route.queryParams, params: this.route.params }).pipe(debounceTime(0)); //debounceTime(0) solves issue when combineLatest emitts two values when navigating

    route$.subscribe(context => {
      console.warn('Subscription');

      this.routeValue = (context.params['categoryRoute']) ? context.params['categoryRoute']! : context.params['mainCategoryRoute']!;
      this.page = (isNaN(context.qparams['page'])) ? 1 : Number(context.qparams['page']);

      this.initProductsAndCount();
    });
  }

  public onLikeClick(productId: number) {
    this.oidcSecurityService.getAccessToken().subscribe(accessToken => {
      this.productsService.likeProduct(accessToken, productId).subscribe(a => console.log(a));
    });
  }

  private initProperties(): void {
    if (!this.products)
      return;

    this.products.forEach(pr => {
      pr.tagValues.forEach(tv => {
        if (tv.name === 'Condition')
          pr.condition = tv.value;
        if (tv.name === 'Price' || tv.name === 'Salary')
          pr.price = tv.value;
      })
    });
  }

  private initProductsAndCount(): void {
    this.productsService
      .getProductsByCategoryAndPage(this.routeValue, this.page)
      .subscribe(data => {
        this.products = data;
        console.warn('page: ' + this.page + ' | route: ' + this.routeValue + ' | products: ' + JSON.stringify(this.products));
        this.initProperties();
      });

    this.productsService
      .getProductsCountByCategory(this.routeValue)
      .subscribe(data => {
        this.productsCount = data;
        this.pagesCount = this.paginationService.calculatePagesCount(data);
        this.paginationArray = this.paginationService.getPaginationArray(this.page, this.pagesCount);
      });
  }
}