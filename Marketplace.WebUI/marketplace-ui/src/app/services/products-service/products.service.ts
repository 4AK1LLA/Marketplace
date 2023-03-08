import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { ProductDto } from '../../dto/product.dto';
import { ProductDetailsDto } from 'src/app/dto/product-details.dto';
import { BasicInfo, TagValue } from 'src/app/models/models';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  public getProductsByCategoryAndPage = (route: string, page: number): Observable<ProductDto[]> =>
    this.http.get<ProductDto[]>(`${environment.baseApiUrl}/Product/Get/${route}`, { params: { pageNumber: page } });

  public getProductsCountByCategory = (route: string): Observable<number> =>
    this.http.get<number>(`${environment.baseApiUrl}/Product/GetCount/${route}`);

  public getProductDetails = (id: number): Observable<ProductDetailsDto> =>
    this.http.get<ProductDetailsDto>(`${environment.baseApiUrl}/Product/${id}`);

  public postProduct(accessToken: string, basicInfo: BasicInfo, tagValues: TagValue[]) {

    let httpOptions = this.getHttpOptions(accessToken);

    let body = {
      title: basicInfo.title,
      description: basicInfo.description,
      location: basicInfo.location,
      categoryId: basicInfo.categoryId,
      tagValues
    }

    return this.http.post(`${environment.baseApiUrl}/Product`, body, httpOptions);
  }

  public likeProduct(accessToken: string, id: number) {

    let httpOptions = this.getHttpOptions(accessToken);

    this.http.put(`${environment.baseApiUrl}/Like/${id}`, null, httpOptions);
  }

  private getHttpOptions(accessToken: string) {
    return { headers: new HttpHeaders({ Authorization: 'Bearer ' + accessToken }) }
  }
}