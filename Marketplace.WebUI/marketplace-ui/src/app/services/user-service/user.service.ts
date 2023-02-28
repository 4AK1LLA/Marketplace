import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  createUser(accessToken: string, idToken: string) {
    let httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + accessToken,
        idToken
      })
    }

    return this.http.post(`${environment.baseApiUrl}/User`, null, httpOptions);
  }
}
