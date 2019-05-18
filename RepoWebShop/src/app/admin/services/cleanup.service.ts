import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ICleanupData {
  cartDataRows: number;
  cartDatesRows: number;
  cartProductsRows: number;
  cartLunchesRows: number;
  lunchesRows: number;
}

@Injectable({
  providedIn: 'root'
})

export class CleanupService {

  constructor(private http: HttpClient) { }

  cleanSessionData = (entries: number) =>
    this.http.post('/api/AdminData/ClearUnusedSessionData/' + entries, null) as Observable<ICleanupData>
}
