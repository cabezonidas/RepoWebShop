import { Injectable, Optional, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})

export class SmsService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  isValidMobile = (mobile: string): Observable<boolean> =>
    (this.http.get(this.api + '/_sms/isValidMobile/' + mobile) as Observable<boolean>)
}
