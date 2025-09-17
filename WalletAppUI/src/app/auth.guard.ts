import { Injectable } from '@angular/core';
import { CanActivate, Route, Router } from '@angular/router';

import { AuthServiceService } from './services/auth.service.service';

@Injectable ({
  providedIn:'root'
})

export class AuthGuard implements CanActivate {
  constructor ( private Authserices : AuthServiceService , private router : Router){} 

   canActivate(): boolean {
    if(this.Authserices.isLoggedIn()) {
    return true;
    }
    else{
      this.router.navigate(['/login']);
      return false
    }
   }
}