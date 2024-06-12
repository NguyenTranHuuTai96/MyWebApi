import { HttpClient, HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map, switchMap, throwError } from 'rxjs';
import { DbcontextService } from './dbcontext.service';
import { JwtModel } from '../Models/JwtModel';
import { BaseApiService } from './base-api.service';
import { data } from 'jquery';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class TokenheaderService implements HttpInterceptor {


  constructor(private dbContext: DbcontextService, private http: BaseApiService) { }
  public url = 'Authentication/refresh-token';
  public jwtModel = new JwtModel();
  private isRefreshing = false;


  CreatetokenNew(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.jwtModel.AccessToken = sessionStorage.getItem('access-token') ?? '';
      this.jwtModel.RefreshToken = sessionStorage.getItem('refresh-token') ?? '';
      return this.http.post(this.url, this.jwtModel).pipe(
        switchMap((data: any) => {
          this.isRefreshing = false;
          sessionStorage.setItem('access-token', data.AccessToken);
          let authorization = `Bearer ${sessionStorage.getItem('access-token')}`;

          return next.handle(req.clone({ setHeaders: { authorization } }));
        }),
        catchError((error) => {
          this.isRefreshing = false;

          // if (error.status == '403') {
          //   this.eventBusService.emit(new EventData('logout', null));
          // }

          return throwError(() => error);
        })
      );

    }

    return next.handle(req);





  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let authorization = `Bearer ${sessionStorage.getItem('access-token')}`;

    return next.handle(req.clone({
      setHeaders: { authorization }
    })).pipe(catchError(
      error => {
        if (error instanceof HttpErrorResponse && error.status == 401)  return this.CreatetokenNew(req, next) as Observable<HttpEvent<any>>;
        return throwError(() => error.message)
      }
    ));
  }
}
