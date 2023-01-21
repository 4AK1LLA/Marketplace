import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { MainCategoryDto } from '../dto/main-category.dto';

@Injectable({
  providedIn: 'root'
})
export class MainCategoriesService {

  constructor(private http: HttpClient) { }

  getAll = (): Observable<MainCategoryDto[]> => this.http.get<MainCategoryDto[]>(`${environment.baseApiUrl}/MainCategory`);
  
}