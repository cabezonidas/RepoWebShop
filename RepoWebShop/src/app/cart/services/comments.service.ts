import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from '../../../../node_modules/rxjs/operators';
import { Observable } from '../../../../node_modules/rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  constructor(private http: HttpClient) {}
  
  loadComments = () => this.http.get<string>('/api/_cartComments/get')
    .pipe(catchError((error: any) => Observable.throw(error.json())));
  
  addComments = (comments: string) => this.http.post<string>('/api/_cartComments/add', comments)
    .pipe(catchError((error: any) => Observable.throw(error.json())));
}
