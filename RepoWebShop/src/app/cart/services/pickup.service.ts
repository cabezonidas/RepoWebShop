import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPickupOption } from '../components/pickup/interfaces/ipickup-option';
import { IPickupDate } from '../interfaces/ipickup-date';

@Injectable({
  providedIn: 'root'
})
export class PickupService {

  constructor(private http: HttpClient) { }

  loadPickupOptions = () => (this.http.get('/api/_cartPickup/pickUpOptionsByDay') as Observable<Array<IPickupOption>>);
  setPickupOption = (ticksId: string) => this.http.post<IPickupDate>('/api/_cartPickup/setPickUpOption/' + ticksId, null);
  getPickupOption = () => this.http.get<IPickupDate>('/api/_cartPickup/getPickUpOption');
  preparationTime = () => this.http.get<number>('/api/_cartPickup/preparationTime');
}
