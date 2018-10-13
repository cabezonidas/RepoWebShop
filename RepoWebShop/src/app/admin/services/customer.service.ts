import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICustomer } from '../interfaces/icustomer';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  getAll = () => this.http.get<ICustomer[]>(this.api + '/_account/getCustomers');
  getEmailActivationCode = (regId: string) =>
    this.http.get(this.api + '/_account/getEmailActivationCode/' + regId, {responseType: 'text'})
  getMobileActivationCode = (regId: string) =>
    this.http.get(this.api + '/_account/getMobileActivationCode/' + regId, {responseType: 'text'})
  activateMobile = (regId: string) => this.http.post(this.api + '/_account/activateMobile/' + regId, null);
}
