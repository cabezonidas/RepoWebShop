import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPickupOption } from '../components/pickup/interfaces/ipickup-option';

@Injectable({
  providedIn: 'root'
})
export class PickupService {

  constructor(private http: HttpClient) { }
  
  loadPickupOptions = () => (this.http.get('/api/_pickup/pickUpOptionsByDay') as Observable<Array<IPickupOption>>);
}
