import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from 'src/app/dto/product.dto';
import { PaginationService } from 'src/app/services/pagination-service/pagination.service';
import { ProductsService } from 'src/app/services/products-service/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  private readonly _maxProductsPerPage: number = 16;
  productsCount!: number;
  products: ProductDto[] = [];
  routeValue!: string;

  //pagination
  page!: number;
  pagesCount!: number;
  paginationArray: any[] = [];

  constructor(private productsService: ProductsService, private route: ActivatedRoute, private paginationService: PaginationService) { }

  ngOnInit(): void {
    this.route.url.subscribe(url => {
      this.route.queryParams.subscribe(params => {
        this.page = (params['page'] === undefined) ? 1 : params['page'];
      });
      this.routeValue = url[url.length - 1].path;
      this.initProductsAndCount();
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
        console.log(this.products)
        this.initProperties();
      });

    this.productsService
      .getProductsCountByCategory(this.routeValue)
      .subscribe(data => {
        this.productsCount = data;
        this.pagesCount = (data % this._maxProductsPerPage == 0)
          ? data / this._maxProductsPerPage
          : Math.floor(data / this._maxProductsPerPage) + 1;
        this.paginationArray = this.paginationService.getPaginationArray(this.page, this.pagesCount);
      });
  }
}