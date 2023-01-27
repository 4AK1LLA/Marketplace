import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductDto } from 'src/app/dto/product.dto';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  products: ProductDto[] = [];
  routeValue!: string;

  constructor(private productsService: ProductsService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let mainCategoryRoute = this.route.snapshot.paramMap.get('mainCategoryRoute')!;
    let categoryRoute = this.route.snapshot.paramMap.get('categoryRoute')!;

    this.routeValue = (categoryRoute) ? categoryRoute : mainCategoryRoute;
    this.initProducts();
  }

  private initProducts = () => this.productsService.getProductsByCategory(this.routeValue).subscribe(data => this.products = data);
}