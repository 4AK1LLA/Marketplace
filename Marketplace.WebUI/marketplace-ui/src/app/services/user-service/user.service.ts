import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'src/environments/environment';
import { UserDto } from 'src/app/dto/user.dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getOrCreateUser(accessToken: string, idToken: string) {
    let httpOptions = {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + accessToken,
        idToken
      })
    }

    return this.http.get<UserDto>(`${environment.baseApiUrl}/User`, httpOptions);
  }
}
