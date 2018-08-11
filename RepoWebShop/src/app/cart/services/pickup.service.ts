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
  setPickupOption = (ticksId: string) => this.http.post<IPickupOption>('/api/_cartPickup/setPickUpOption', ticksId);
  getPickupOption = () => this.http.get<IPickupDate>('/api/_cartPickup/getPickUpOption');
}
