import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPickupOption } from '../components/pickup/interfaces/ipickup-option';
import { IPickupDate } from '../interfaces/ipickup-date';
import { APP_BASE_HREF } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class PickupService {
  public api = 'api';
  constructor(private http: HttpClient, @Optional() @Inject(APP_BASE_HREF) origin: string) {
    this.api = `${origin ? origin : ''}${this.api}`;
  }

  loadPickupOptions = () => (this.http.get(this.api + '/_cartPickup/pickUpOptionsByDay') as Observable<Array<IPickupOption>>);
  getPickupOption = () => this.http.get<IPickupDate>(this.api + '/_cartPickup/getPickUpOption');
  preparationTime = () => this.http.get<number>(this.api + '/_cartPickup/preparationTime');
  setPickupOption = (ticksId: string) =>
    this.http.post<IPickupDate>(this.api + '/_cartPickup/setPickUpOption/' + ticksId, null)
}
