import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from '../../../../node_modules/rxjs/operators';
import { Observable } from '../../../../node_modules/rxjs';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    }),
    responseType: 'text' as 'text'
  };

  loadComments = () => this.http.get(this.api + '/_cartComments/get', {responseType: 'text'})
    .pipe(catchError((error: any) => {
      return Observable.throw(error.json());
    }))

  addComments = (comments: string) =>
    this.http.post(this.api + '/_cartComments/add', JSON.stringify(comments), this.httpOptions)
    .pipe(catchError((error: any) => {
      return Observable.throw(error.json());
    }))
}
