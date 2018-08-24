import { Component, OnInit, ViewChild, ElementRef, NgZone, OnDestroy, AfterViewInit, EventEmitter, Output } from '@angular/core';
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
  savingDelivery = false;
  deliverySaved = false;
  saveDelivery$ = new Subscription();
  deliveryGet$ = new Subscription();
  canDeliver$ = new Subscription();
  clearDelivery$ = new Subscription();
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
  @Output() next = new EventEmitter<void>();



  constructor(private mapsAPILoader: MapsAPILoader, private ngZone: NgZone, private delivery: DeliveryService) { }

  ngOnInit() {
    this.canDeliver$ = this.delivery.canDeliver().subscribe(can => this.canDeliver = can);
      this.mapsAPILoader.load().then(() => {
          this.autocomplete = new google.maps.places.Autocomplete(
            this.searchElement.nativeElement, { types: ['address'], componentRestrictions: { country: 'ar' }});
          this.autocomplete.addListener('place_changed', () => this.onPlaceSelection());
        }
      );
    this.deliveryGet$ = this.delivery.get().subscribe((address) => {
      if (address) {
        this.address = address;
        this.deliverySaved = true;
      }
    });
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
          this.savingDelivery = true;
          this.saveDelivery$ = this.delivery.saveDelivery(this.address).subscribe((address) => {
            console.log(address);
            this.savingDelivery = false;
            this.address = address;
            this.deliverySaved = true;
          }, (err) => {
            this.deliverySaved = false;
            this.savingDelivery = false;
            console.log(err);
          });
        }
      }
    });
  }

  distance = (): number => this.delivery.getDistanceFromLatLonInKm(
    this.storeLatitude, this.storeLongitude, this.address.latitude, this.address.longitude)

  inZone = (): boolean => this.distance() <= this.deliveryKmRadius;


  ngOnDestroy() {
    this.saveDelivery$.unsubscribe();
    this.deliveryGet$.unsubscribe();
    this.canDeliver$.unsubscribe();
    this.clearDelivery$.unsubscribe();
    this.deliveryInstrStream$.unsubscribe();
  }

  clear() {
    this.saveDelivery$.unsubscribe();
    this.searchSelected = false;
    this.address = null;
  }

  clearDelivery() {
    this.searchElement.nativeElement.value = '';
    this.savingDelivery = true;
    this.clearDelivery$ = this.delivery.clearDelivery().subscribe(() => {
      this.savingDelivery = false;
      this.deliverySaved = false;
    }, (err) => {
      this.savingDelivery = false;
      console.log(err);
    });
  }

  proceed = () => this.next.emit();

  ngAfterViewInit() {
    this.deliveryInstrStream$ = fromEvent(this.addressInstructions.nativeElement, 'keyup')
      .pipe(
        map((e: any) => e.target.value as string),
        debounceTime(500),
        distinctUntilChanged(),
        tap(instructions => this.address.deliveryInstructions = instructions),
        switchMap(() => this.delivery.updateInstructions(this.address))
      )
      .subscribe(res => console.log(res));
  }
}
