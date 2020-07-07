import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { BaseService } from './../../shared/services/base.service';
import { Login } from './../models/login';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService extends BaseService {

  private token: any;
  private user: any;
  public returnUrl: string;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private http: HttpClient) {
    super();
    this.setToken();
  }

  signinUser(login: Login) {
      return this.http.post(`${this.API}/Security/Login`, login);
  }

  getToken(): any {
    this.setToken();
    return this.token;
  }

  getUser(): any {
    return this.user;
  }

  isAuthenticated(returnUrl: string) {
    if(this.token === null){
      this.returnUrl = returnUrl;
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }

  loginSuccess(response: any){
    localStorage.setItem('uptec.token', response.data.access_token);
    localStorage.setItem('uptec.user', JSON.stringify(response.data.user));
    this.setToken();
  }

  logout() {   
    localStorage.removeItem('uptec.token');
    localStorage.removeItem('uptec.user');
    this.token = null;
    this.user = null;
    this.returnUrl = "";
    this.router.navigate(['/login']);
  }

  private setToken(){
    this.token = localStorage.getItem('uptec.token');
    this.user = JSON.parse(localStorage.getItem('uptec.user'));
  }
}
