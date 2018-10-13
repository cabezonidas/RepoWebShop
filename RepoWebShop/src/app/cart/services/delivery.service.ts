import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DeliveryAddress } from '../classes/delivery-address';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  canDeliver = () => this.http.get<boolean>(this.api + '/_cartDelivery/canDeliver');
  guess = (address: string) => this.http.get<any>(this.api + '/_cartDelivery/guess/' + address);
  clearDelivery = () => this.http.delete<void>(this.api + '/_cartDelivery/remove');
  get = () => this.http.get<DeliveryAddress>(this.api + '/_cartDelivery/get');
  updateInstructions = (instructions: DeliveryAddress) =>
    this.http.post<DeliveryAddress>(this.api + '/_cartDelivery/updateInstructions', instructions)
  saveDelivery = (addresss: DeliveryAddress) =>
    this.http.post<DeliveryAddress>(this.api + '/_cartDelivery/saveDelivery', addresss)

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

  zipCode = (place: google.maps.GeocoderAddressComponent[]): string => {
    const code = place.find(x => x.types.includes('postal_code'));
    return code ? code.long_name : '';
  }

  streetNumber = (place: google.maps.GeocoderAddressComponent[]): string => {
    const code = place.find(x => x.types.includes('street_number'));
    return code ? code.long_name : '';
  }

  streetName = (place: google.maps.GeocoderAddressComponent[]): string => {
    const code = place.find(x => x.types.includes('route'));
    return code ? code.long_name : '';
  }
}
