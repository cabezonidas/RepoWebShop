import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeliveryAddress } from '../../cart/classes/delivery-address';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  constructor(private http: HttpClient) { }

  get = (id: number) => this.http.get<DeliveryAddress>('/api/_delivery/get/' + id);
}
