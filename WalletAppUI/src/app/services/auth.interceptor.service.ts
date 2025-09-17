import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private jwthelper : JwtHelperService) { }

   intercept( req : HttpRequest <any> , next : HttpHandler ): Observable<HttpEvent<any>>{
    const token = localStorage.getItem('token');

    if(token && !this.jwthelper.isTokenExpired(token)){
     const  cloned =req.clone({
        setHeaders:{Authorization :` Bearer ${token}`}
      });
      return next.handle(cloned)
    }
     return next.handle(req);
   }
}
