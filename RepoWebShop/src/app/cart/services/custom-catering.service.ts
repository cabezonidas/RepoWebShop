import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICatering } from '../../catering/interfaces/ICatering';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class CustomCateringService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  clearSessionCatering = (): Observable<void> => this.http.delete<void>(this.api + '/_cartCustomCatering/clearSessionCatering');
  loadSessionCatering = (): Observable<ICatering> =>
    this.http.get<ICatering>(this.api + '/_cartCustomCatering/loadSessionCatering')
}
