import { Component, OnInit, ViewChild, ElementRef, NgZone, OnDestroy,
  AfterViewInit, EventEmitter, Output, Input, OnChanges } from '@angular/core';
import { MapsAPILoader } from '@agm/core';
import { DeliveryAddress } from '../../classes/delivery-address';
import { DeliveryService } from '../../services/delivery.service';
import { Observable, Subscription, fromEvent, of } from 'rxjs';
import { debounceTime, map, distinctUntilChanged, switchMap, tap } from 'rxjs/operators';
import * as fromStore from '../../store';
import { Store, select } from '@ngrx/store';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.scss']
})
export class DeliveryComponent implements OnInit, OnDestroy, AfterViewInit, OnChanges, OnInit {
  canDeliver$ = new Subscription();
  deliveryInstrStream$ = new Subscription();
  deliveryStream$ = new Subscription();
  canDeliver = true;
  storeLatitude = -34.625265;
  storeLongitude = -58.434483;

  centerDeliveryLatitude = -34.607026;
  centerDeliveryLongitude = -58.444537;
  deliveryKmRadius = 4.5;

  autocomplete: google.maps.places.Autocomplete;
  place: google.maps.places.PlaceResult;
  address: DeliveryAddress;
  searchSelected = false;
  outOfZone = true;
  searchDisabled = false;
  fullAddress = false;

  @ViewChild('search') public searchElement: ElementRef;
  @ViewChild('locate') public locateElement: ElementRef;
  @ViewChild('addressInstr') public addressInstructions: ElementRef;

  @Input() deliveryAddress: DeliveryAddress;
  @Input() deliveryLoaded: boolean;
  @Input() deliveryLoading: boolean;
  @Input() totalWithoutDiscount: number;

  @Output() next = new EventEmitter<void>();
  @Output() addDelivery = new EventEmitter<DeliveryAddress>();
  @Output() updateInstructions = new EventEmitter<DeliveryAddress>();
  @Output() removeDelivery = new EventEmitter<void>();



  constructor(private store: Store<fromStore.CartState>,
    private mapsAPILoader: MapsAPILoader, private ngZone: NgZone, private delivery: DeliveryService) { }

  ngOnInit() {
    this.canDeliver$ = this.store.pipe(
      select(fromStore.getTotal),
      switchMap(() => this.delivery.canDeliver())
    ).subscribe(can => this.canDeliver = can);

    this.mapsAPILoader.load().then(() => {
        this.autocomplete = new google.maps.places.Autocomplete(
          this.searchElement.nativeElement, { types: ['address'], componentRestrictions: { country: 'ar' }});
        this.autocomplete.addListener('place_changed', () => this.onPlaceSelection());
      }
    );
  }

  ngOnChanges() {
  }

  onPlaceSelection() {
    this.ngZone.run(() => {
      this.place = this.autocomplete.getPlace();
      if (this.place.geometry === undefined || this.place.geometry === null) {
        this.clear();
      } else {
        const streetNumber = this.delivery.streetNumber(this.place.address_components);
        const streetName = this.delivery.streetName(this.place.address_components);
        const lat = this.place.geometry ? this.place.geometry.location.lat() : 0;
        const lng = this.place.geometry ? this.place.geometry.location.lng() : 0;
        this.trySaveDelivery(streetNumber, streetName, lat, lng);
      }
    });
  }

  trySaveDelivery = (streetNumber: string, streetName: string, lat: number, lng: number) => {
    this.address = new DeliveryAddress(streetNumber, streetName, lat, lng);
    this.searchSelected = true;
    this.fullAddress = !!this.address.streetName && !!this.address.streetNumber;
    this.outOfZone = !this.inZone();
    if (this.fullAddress && !this.outOfZone) {
      this.addDelivery.emit(this.address);
    }
  }

  distance = (): number => this.delivery.getDistanceFromLatLonInKm(
    this.centerDeliveryLatitude, this.centerDeliveryLongitude, this.address.latitude, this.address.longitude)

  inZone = (): boolean => this.distance() <= this.deliveryKmRadius;


  ngOnDestroy() {
    this.canDeliver$.unsubscribe();
    this.deliveryInstrStream$.unsubscribe();
    this.deliveryStream$.unsubscribe();
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
      .subscribe();

    this.deliveryStream$ = fromEvent(this.locateElement.nativeElement, 'click')
      .pipe(
        map(() => (<HTMLInputElement>document.getElementById('search')).value),
        switchMap(address => this.delivery.guess(address))
      )
      .subscribe(place => {
        if (place) {
          try {
            const result = place.PlaceDetailsResponse.result;
            const streetNumber = result.address_component.find(x => x.type === 'street_number').long_name;
            const streetName = result.address_component.find(x => x.type === 'route').long_name;
            this.trySaveDelivery(streetNumber, streetName, result.geometry.location.lat, result.geometry.location.lng);
          } catch {
            this.fullAddress = false;
          }
        } else {
          this.fullAddress = false;
        }
    });
  }
}
