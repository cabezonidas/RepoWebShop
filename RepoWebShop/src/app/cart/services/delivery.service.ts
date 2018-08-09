import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeliveryAddress } from '../classes/delivery-address';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  constructor(private http: HttpClient) { }

  canDeliver = () => this.http.get<boolean>('/api/_delivery/canDeliver');
  clearDelivery = () => this.http.delete<boolean>('/api/_delivery/remove');
  updateInstructions = (instructions: DeliveryAddress) => this.http.post<DeliveryAddress>('/api/_delivery/updateInstructions', instructions);
  saveDelivery = (addresss: DeliveryAddress) => this.http.post<DeliveryAddress>('/api/_delivery/saveDelivery', addresss);
  get = () => this.http.get<DeliveryAddress>('/api/_delivery/get');

  getDistanceFromLatLonInKm(lat1, lon1, lat2, lon2): number {
    const R = 6371; // Radius of the earth in km
    const dLat = this.deg2rad(lat2 - lat1);  // deg2rad below
    const dLon = this.deg2rad(lon2 - lon1);
    const a = Math.sin(dLat / 2) * Math.sin(dLat / 2)
      + Math.cos(this.deg2rad(lat1)) * Math.cos(this.deg2rad(lat2)) * Math.sin(dLon / 2) * Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    const d = R * c; // Distance in km
    return d;
  }

  private deg2rad = (deg) => deg * (Math.PI / 180);

}