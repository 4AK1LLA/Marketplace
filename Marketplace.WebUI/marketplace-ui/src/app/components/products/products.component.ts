import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from 'src/app/dto/product.dto';
import { ProductsService } from 'src/app/services/products-service/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  products: ProductDto[] = [];
  routeValue!: string;
  page!: number;

  constructor(private productsService: ProductsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.url.subscribe(url => {
      this.routeValue = url[url.length - 1].path;
      this.initProducts();
    });

    this.route.queryParams.subscribe(params => {
      this.page = (params['page'] === undefined) ? 1 : params['page'];
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

  private initProducts(): void {
    this.productsService
      .getProductsByCategory(this.routeValue)
      .subscribe(data => {
        this.products = data;
        console.log(this.products)
        this.initProperties();
      });
  }
}