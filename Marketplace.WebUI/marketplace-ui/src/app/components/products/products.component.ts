import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  paginationHref: string = '';

  constructor(
    private productsService: ProductsService,
    private route: ActivatedRoute,
    private paginationService: PaginationService,
  ) { }

  ngOnInit(): void { //now Im using routerLink (removed hrefs) wich doesnt require page reloading, so this hook and observable subscription are executing during whole component lifetime
    let route$ = combineLatest({ qparams: this.route.queryParams, params: this.route.params }).pipe(debounceTime(0)); //debounceTime(0) solves issue when combineLatest emitts two values when navigating

    route$.subscribe(context => {
      console.warn('Subscription');

      this.routeValue = (context.params['categoryRoute']) ? context.params['categoryRoute']! : context.params['mainCategoryRoute']!;
      this.page = (isNaN(context.qparams['page'])) ? 1 : Number(context.qparams['page']);

      this.initProductsAndCount();
    });

    //Test code (will be deleted in next commit)
    // var observable = combineLatest({ qparams: this.route.queryParams, params: this.route.params })
    //   .pipe(withLatestFrom(this.route.params));

    //   observable.subscribe(context => {
    //     console.warn('Subscribed')

    //   let routeValue = (context[0].params['categoryRoute']) ? context[0].params['categoryRoute']! : context[0].params['mainCategoryRoute']!;
    //   let page = (context[0].qparams['page'] === null) ? 1 : Number(context[0].qparams['page']);

    //   console.warn('new page: ' + page + ' new route: ' + routeValue)
    //   // this.routeValue = routeValue;
    //   // this.page = page;
    //   // this.initProductsAndCount(routeValue, page);
    // });

    // this.route.params.pipe(
    //   withLatestFrom(this.route.queryParams)
    // ).subscribe(([src1, src2]) => { 
    //   console.warn('withLatestFrom triggered')
    //   console.log(src1, src2) 
    //   let routeValue = (src1['categoryRoute']) ? src1['categoryRoute']! : src1['mainCategoryRoute']!;
    //   let page = (src2['page'] === null) ? 1 : Number(src2['page']);

    //   this.page = page;
    //   this.routeValue = routeValue;
    //   this.initProductsAndCount(routeValue, page);
    // })//end

    // var comb = combineLatest({ qparams: this.route.queryParams, params: this.route.params });

    // comb.subscribe(context => {
    //   console.warn('Comb observable triggered')

    //   let routeValue = (context.params['categoryRoute']) ? context.params['categoryRoute']! : context.params['mainCategoryRoute']!;
    //   let page = (context.qparams['page'] === null) ? 1 : Number(context.qparams['page']);

    //   console.warn('new page: ' + page + ' new route: ' + routeValue)
    //   this.routeValue = routeValue;
    //   this.page = page;
    //   this.initProductsAndCount(routeValue, page);
    // });

    // this.route.paramMap.subscribe(params => {
    //   console.warn('Params(route) changes')

    //   let newRoute = (params.get('categoryRoute')) ? params.get('categoryRoute')! : params.get('mainCategoryRoute')!; 

    //   if (this.page === undefined) {
    //     console.warn('Changed page to 1 bc it is undefined')
    //     this.page = 1;
    //   }

    //   this.routeValue = newRoute;
    //   this.initProductsAndCount();
    // });

    // this.route.queryParamMap.subscribe(query => {
    //   console.warn('Query(page) changes')

    //   let newPage = (query.get('page') === null) ? 1 : Number(query.get('page'));

    //   this.page = newPage;
    //   this.initProductsAndCount();
    // })

    // this.route.paramMap.subscribe(params => {
    //   console.warn('Params(route) changes')

    //   this.routeValue = (params.get('categoryRoute')) ? params.get('categoryRoute')! : params.get('mainCategoryRoute')!;
    //   console.log('page: '+this.page+' & route: '+this.routeValue)
    // })
    // this.route.queryParamMap.subscribe(query => {
    //   console.warn('Query(page) changes')

    //   this.page = (query.get('page') === null) ? 1 : Number(query.get('page'));
    //   console.log('page: '+this.page+' & route: '+this.routeValue)
    // })

    // this.initProductsAndCount();

    // this.route.paramMap.subscribe(params => {
    //   this.route.queryParamMap.subscribe(query => {
    //     this.page = (query.get('page') === null) ? 1 : Number(query.get('page'));
    //     this.initProductsAndCount();
    //     console.log('current page: ' + this.page);
    //   })
    //   this.routeValue = (params.get('categoryRoute')) ? params.get('categoryRoute')! : params.get('mainCategoryRoute')!;
    //   this.initProductsAndCount();
    //   console.log('current route: ' + this.routeValue);
    // });
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