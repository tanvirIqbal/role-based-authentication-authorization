import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Constants } from '../_helpers/constants';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const user = localStorage.getItem(Constants.USER_KEY) ? JSON.parse(localStorage.getItem(Constants.USER_KEY) || "") as User
    : new User("","","");

    if (user.email) {
      return true; 
    } else {
      this.router.navigate(["/login"]);
      return false;
    }
  }
}
