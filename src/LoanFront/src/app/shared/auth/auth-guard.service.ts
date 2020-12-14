import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/services/auth/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(public auth: AuthService, public router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree {
    if (!this.auth.isLogged()) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
