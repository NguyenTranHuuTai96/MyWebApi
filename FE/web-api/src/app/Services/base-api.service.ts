import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, delay, map, throwError } from 'rxjs';
import { environment } from '../Enviroments/Enviroment';
//import { NotificationService } from './notification.service';


@Injectable({
  providedIn: 'root'
})
export class BaseApiService {
  private headers: HttpHeaders;
  constructor(private http: HttpClient,
	//private notification: NotificationService
) {
    this.headers = new HttpHeaders({
			'Content-Type': 'application/json',
			//'Authorization':
		 });
   }
   GetData(api: string){
    return new Promise<any>((resolve, reject) => {
      this.http.get(api).pipe(map(data => data)).subscribe(data => {
        if(!data) {
          resolve([]);
          return;
        }
        resolve(data);
      })
    });
  }
  PutData(api: string, model:any){
    return new Promise<any>((resolve, reject) => {
      this.http.put(api,model, { headers: this.headers }).subscribe(data => {
        if (!data) {
          resolve([]);
          return;
        }
        resolve(data);
      });
    });
  }
  public getUrl(url:string) {
		if (url.indexOf('/') == 0) {
			url = url.substring(1);
		}
		var domain=window.location.protocol+"//"+window.location.host;
		if(!environment.SERVER_API_URL.startsWith('http://') && !environment.SERVER_API_URL.startsWith('https://')){
			environment.SERVER_API_URL=domain + environment.SERVER_API_URL;
		}
		return environment.SERVER_API_URL + url;
	}
  public get<T=any>(url: string): Observable<T> {
	var link = this.getUrl(url);
		return this.http.get<T>(link,{ headers: this.headers }).pipe(
			delay(10),
			map((data: T) => (data as T)),
			catchError(this.handleError)
		);
	}
  public post<T>(url: string, body: any): Observable<any> {
		let _url = this.getUrl(url);
		let data = JSON.stringify(body);
		return this.http.post<T>(_url, data, { headers: this.headers }).pipe(
			map((data: T) => (data as T)),
			catchError(this.handleError)
		);;
	}
  public put<T>(url: string, body: any): Observable<T> {
		let _url = this.getUrl(url);
		let data = JSON.stringify(body);
		return this.http.put<T>(_url, data, { headers: this.headers }).pipe(
			map((data: T) => (data as T)),
			catchError(this.handleError)
		);
	}
  public delete(url: string): Observable<any> {
		let _url = this.getUrl(url);
		let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
		return this.http.delete(_url,  { headers: headers }).pipe(
			map((data: any) => (data)),
			catchError(this.handleError)
		);
	}
  private handleError(error: any) {
		// In a real world app, we might use a remote logging infrastructure
		// We'd also dig deeper into the error to get a better message
		let errMsg = (error.message) ? error.message :
			error.status ? `${error.status} - ${error.statusText}` : 'Server error';
		let errObj = { error: { code: -999, message: errMsg } };
		if(error.status!=401){
			//this.notification.ShowMessage('error',errMsg)
		}
		return throwError(()=> error.message);
	}
}
