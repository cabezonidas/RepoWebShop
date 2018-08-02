import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IItem } from '../../products/interfaces/iitem';

@Injectable({
  providedIn: 'root'
})
export class CateringService {

  constructor(private http: HttpClient) { }

  getItems = (): Observable<IItem[]> => this.http.get<IItem[]>('/api/_products/cateringItems');
}
