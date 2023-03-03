import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { MainCategoryDto, MainCategoryPostDto } from '../../dto/main-category.dto';

@Injectable({
  providedIn: 'root'
})
export class MainCategoriesService {

  private baseRequestUrl: string = `${environment.baseApiUrl}/MainCategory`;

  constructor(private http: HttpClient) { }

  getAll = () => this.http.get<MainCategoryDto[]>(this.baseRequestUrl);

  getAllForPosting = () => this.http.get<MainCategoryPostDto[]>(this.baseRequestUrl + '?posting=true');
  
}