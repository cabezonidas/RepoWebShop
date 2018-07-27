import { Injectable } from '@angular/core';
import { Observable } from '../../../node_modules/rxjs';
import { HttpClient } from '../../../node_modules/@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class SmsService {

  constructor(private http: HttpClient) { }

  isValidMobile = (mobile: string): Observable<boolean> => (this.http.get('/api/_sms/isValidMobile/' + mobile) as Observable<boolean>);
}
