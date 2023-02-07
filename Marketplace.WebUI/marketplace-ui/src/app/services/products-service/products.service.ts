import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { ProductDto } from '../../dto/product.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  getProductsByCategoryAndPage = (route: string, page: number): Observable<ProductDto[]> =>
    this.http.get<ProductDto[]>(`${environment.baseApiUrl}/Product/Get/${route}`, { params: { pageNumber: page } });

  getProductsCountByCategory = (route: string): Observable<number> =>
    this.http.get<number>(`${environment.baseApiUrl}/Product/GetCount/${route}`);
}