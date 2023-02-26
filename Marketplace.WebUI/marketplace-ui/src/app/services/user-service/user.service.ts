import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  createUser(token: string) { 
    let httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + token
      }),
      responseType: 'text' as const
    }
    
    return this.http.post(`${environment.baseApiUrl}/User`, null, httpOptions);
  }
}
