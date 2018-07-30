import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class SmsService {

  constructor(private http: HttpClient) { }

  isValidMobile = (mobile: string): Observable<boolean> => (this.http.get('/api/_sms/isValidMobile/' + mobile) as Observable<boolean>);
}
