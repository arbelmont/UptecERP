import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    
    let isAuth = this.authService.isAuthenticated(state.url);

    if(!isAuth)
      return false;

    var claims: any = route.data[0];

    if (claims !== undefined) {
      let user = this.authService.getUser();

      console.log(user);
      console.log(claims);
      let count = 0;

      claims['claim'].forEach(el => {
        if(user.claims.some(x => x.type.includes("role") && x.value === el))
          count++;
      });
      
      return count > 0;
    }

    return true;
  }
}
