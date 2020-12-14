import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { BaseService } from '../base.service';
import { UserModel } from 'src/app/models/auth/user.model';
import { ApiModel } from 'src/app/models/http/api.model';
import { ResponseAuthModel } from 'src/app/models/auth/response-auth.model';



@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  login(user: UserModel) {
    return this.http
    .post<ApiModel<ResponseAuthModel>>(`${this.routeBase}/connect/login`, user);
  }

  isLogged() {
    let user = localStorage.getItem('user');

    return !(user === null);
  }

  getLogged() {
    let user = localStorage.getItem('user');

    return JSON.parse(user);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem("user");
    localStorage.removeItem("token");
  }

  store(token: string, userName: string) {
    localStorage.setItem('token', token);
    localStorage.setItem('user', userName);
  }
}
