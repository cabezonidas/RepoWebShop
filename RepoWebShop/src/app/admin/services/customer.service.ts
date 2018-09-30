import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ICustomer } from '../interfaces/icustomer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  getAll = () => this.http.get<ICustomer[]>('/api/_account/getCustomers');
  getEmailActivationCode = (regId: string) => this.http.get('/api/_account/getEmailActivationCode/' + regId, {responseType: 'text'});
  getMobileActivationCode = (regId: string) => this.http.get('/api/_account/getMobileActivationCode/' + regId, {responseType: 'text'});
  activateMobile = (regId: string) => this.http.post('/api/_account/activateMobile/' + regId, null);
}
