import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from '../../../../node_modules/rxjs/operators';
import { Observable } from '../../../../node_modules/rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  constructor(private http: HttpClient) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    }),
    responseType: 'text' as 'text'
  };

  loadComments = () => this.http.get('/api/_cartComments/get', {responseType: 'text'})
    .pipe(catchError((error: any) => {
      return Observable.throw(error.json());
    }))

  addComments = (comments: string) => this.http.post('/api/_cartComments/add', JSON.stringify(comments), this.httpOptions)
    .pipe(catchError((error: any) => {
      return Observable.throw(error.json());
    }))
}
