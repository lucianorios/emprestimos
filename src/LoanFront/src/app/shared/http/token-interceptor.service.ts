import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Observable } from 'rxjs';
@Injectable()
export class TokenInterceptorService implements HttpInterceptor {
  constructor(public auth: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let token = this.auth.getToken();

    if(token){
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`,
          'Content-Type': 'application/json'
        }
      });
    } else {
      request = request.clone({
        setHeaders: {
          'Content-Type': 'application/json'
        }
      });
    }


    return next.handle(request);
  }
}
