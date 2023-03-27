import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { ProductDto } from '../../dto/product.dto';
import { ProductDetailsDto } from 'src/app/dto/product-details.dto';
import { CreateProductDto } from 'src/app/dto/create-product.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }

  public getProductsByCategoryAndPage(route: string, page: number, accessToken?: string)
  : Observable<ProductDto[] | { dtos: ProductDto[], likedProductIds: number[] }> 
  {
    if (accessToken) {
      let httpOtions = this.getHttpOptions(accessToken, page);

      return this.http.get<{ dtos: ProductDto[], likedProductIds: number[] }>(`${environment.baseApiUrl}/Product/Get/${route}`, httpOtions);
    }

    return this.http.get<ProductDto[]>(`${environment.baseApiUrl}/Product/Get/${route}`, { params: { pageNumber: page } });
  }

  public getProductsCountByCategory = (route: string): Observable<number> =>
    this.http.get<number>(`${environment.baseApiUrl}/Product/GetCount/${route}`);

  public getProductDetails(id: number, accessToken?: string): Observable<ProductDetailsDto> {
    if (accessToken) {
      let httpOptions = this.getHttpOptions(accessToken);

      return this.http.get<ProductDetailsDto>(`${environment.baseApiUrl}/Product/${id}`, httpOptions);
    }

    return this.http.get<ProductDetailsDto>(`${environment.baseApiUrl}/Product/${id}`);
  }

  public postProduct(accessToken: string, dto: CreateProductDto) {

    let httpOptions = this.getHttpOptions(accessToken);

    return this.http.post(`${environment.baseApiUrl}/Product`, dto, httpOptions);
  }

  public likeProduct(accessToken: string, id: number) {

    let httpOptions = this.getHttpOptions(accessToken);

    return this.http.put<boolean>(`${environment.baseApiUrl}/Like/${id}`, null, httpOptions);
  }

  public getLikedProducts(accessToken: string) {
    let httpOptions = this.getHttpOptions(accessToken);

    return this.http.get<ProductDto[]>(`${environment.baseApiUrl}/Like`, httpOptions);
  }

  public removeAllLikes(accessToken: string) {
    let httpOptions = this.getHttpOptions(accessToken);

    return this.http.delete(`${environment.baseApiUrl}/Like`, httpOptions);
  }

  private getHttpOptions(accessToken: string, page?: number) {
    if (page) {
      return { headers: new HttpHeaders({ Authorization: 'Bearer ' + accessToken }),  params: { pageNumber: page } }
    }

    return { headers: new HttpHeaders({ Authorization: 'Bearer ' + accessToken }) }
  }
}