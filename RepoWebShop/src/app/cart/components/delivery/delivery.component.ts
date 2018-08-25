import { Component, OnInit, ViewChild, ElementRef, NgZone, OnDestroy, AfterViewInit, EventEmitter, Output, Input } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { DeliveryAddress } from '../../classes/delivery-address';
import { DeliveryService } from '../../services/delivery.service';
import { Observable, Subscription, fromEvent, of } from 'rxjs';
import { debounceTime, map, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.scss']
})
export class DeliveryComponent implements OnInit, OnDestroy, AfterViewInit {
  canDeliver$ = new Subscription();
  deliveryInstrStream$ = new Subscription();
  canDeliver = true;
  storeLatitude = -34.625265;
  storeLongitude = -58.434483;
  deliveryKmRadius = 3;

  autocomplete: google.maps.places.Autocomplete;
  place: google.maps.places.PlaceResult;
  address: DeliveryAddress;
  searchSelected = false;
  outOfZone = true;
  searchDisabled = false;
  fullAddress = false;

  @ViewChild('search') public searchElement: ElementRef;
  @ViewChild('addressInstr') public addressInstructions: ElementRef;

  @Input() deliveryAddress: DeliveryAddress;
  @Input() deliveryLoaded: boolean;
  @Input() deliveryLoading: boolean;

  @Output() next = new EventEmitter<void>();
  @Output() addDelivery = new EventEmitter<DeliveryAddress>();
  @Output() updateInstructions = new EventEmitter<DeliveryAddress>();
  @Output() removeDelivery = new EventEmitter<void>();



  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone, private delivery: DeliveryService) { }

  ngOnInit() {
    this.canDeliver$ = this.delivery.canDeliver().subscribe(can => this.canDeliver = can);
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
        this.clear();
      } else {
        this.address = new DeliveryAddress(this.place);
        this.searchSelected = true;
        this.fullAddress = !!this.address.zipCode && !!this.address.streetName && !!this.address.streetNumber;
        this.outOfZone = !this.inZone();
        if (this.fullAddress && !this.outOfZone) {
          this.addDelivery.emit(this.address);
        }
      }
    });
  }

  distance = (): number => this.delivery.getDistanceFromLatLonInKm(
    this.storeLatitude, this.storeLongitude, this.address.latitude, this.address.longitude)

  inZone = (): boolean => this.distance() <= this.deliveryKmRadius;


  ngOnDestroy() {
    this.canDeliver$.unsubscribe();
    this.deliveryInstrStream$.unsubscribe();
  }

  clear() {
    this.searchSelected = false;
    this.address = null;
  }

  clearDelivery() {
    this.searchElement.nativeElement.value = '';
    this.removeDelivery.emit();
  }

  ngAfterViewInit() {
    this.deliveryInstrStream$ = fromEvent(this.addressInstructions.nativeElement, 'keyup')
      .pipe(
        map((e: any) => e.target.value as string),
        debounceTime(500),
        distinctUntilChanged(),
        tap(instructions => this.address.deliveryInstructions = instructions),
        tap(() => this.updateInstructions.emit(this.address))
      )
      .subscribe(res => console.log(res));
  }
}
