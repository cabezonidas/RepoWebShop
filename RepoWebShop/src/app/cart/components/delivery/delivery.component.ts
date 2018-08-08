import { Component, OnInit, ViewChild, ElementRef, NgZone } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { DeliveryAddress } from '../../classes/delivery-address';
import { DeliveryService } from '../../services/delivery.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.scss']
})
export class DeliveryComponent implements OnInit {
  canDeliver$: Observable<boolean>;
  storeLatitude = -34.625265;
  storeLongitude = -58.434483;
  deliveryKmRadius = 3;
  canDeliver = false;

  autocomplete: google.maps.places.Autocomplete;
  place: google.maps.places.PlaceResult;
  address: DeliveryAddress;
  searchSelected = false;
  outOfZone = true;
  searchDisabled = false;
  fullAddress = false;

  @ViewChild('search') public searchElement: ElementRef;

  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone, private delivery: DeliveryService) { }

  ngOnInit() {
    this.canDeliver$ = this.delivery.canDeliver();
    this.mapsAPILoader.load().then(() => {
        this.autocomplete = new google.maps.places.Autocomplete(
          this.searchElement.nativeElement, { types: ['address'], componentRestrictions: { country: 'ar' }});
        this.autocomplete.addListener('place_changed', () => this.onPlaceSelection());
      }
    );
  }

  onPlaceSelection() {
    this.ngZone.run(() => {
      this.place = this.autocomplete.getPlace();
      if (this.place.geometry === undefined || this.place.geometry === null) {
        this.searchSelected = false;
        this.address = null;
        return;
      } else {
        this.address = new DeliveryAddress(this.place);
        this.searchSelected = true;
        this.fullAddress = !!this.address.zipCode && !!this.address.streetName && !!this.address.streetNumber;
        this.outOfZone = !this.inZone();
      }
    });
  }

  inZone = (): boolean => this.delivery.getDistanceFromLatLonInKm(
      this.storeLatitude, this.storeLongitude, this.address.latitude, this.address.longitude
    ) <= this.deliveryKmRadius

}
