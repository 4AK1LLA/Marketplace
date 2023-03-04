import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TagDto } from 'src/app/dto/tag.dto';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  constructor(private http: HttpClient) { }

  getTagsByCategory = (categoryId: number) =>
    this.http.get<TagDto[]>(`${environment.baseApiUrl}/Tag?categoryId=${categoryId}`);
}
