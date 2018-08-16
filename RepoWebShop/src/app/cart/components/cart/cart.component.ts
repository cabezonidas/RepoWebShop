import { Component, OnInit, OnDestroy, HostBinding, ViewChild } from '@angular/core';
import { MatDialog, MatStepper } from '@angular/material';
import { Subscription, Observable } from 'rxjs';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';
import { moveIn, fallIn } from '../../../animations/router.animations';
import { Store, select } from '@ngrx/store';
import { ICatering } from '../../../catering/interfaces/ICatering';
import { ICartCatering } from '../../interfaces/icart-catering';
import * as fromStore from '../../store';
import { DeliveryAddress } from '../../classes/delivery-address';
import { IDiscount } from '../../interfaces/idiscount';
import { IPickupOption } from '../pickup/interfaces/ipickup-option';
import { IPickupDate } from '../../interfaces/ipickup-date';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
  animations: [moveIn(), fallIn()]
})

export class CartComponent implements OnInit, OnDestroy {

  constructor(public dialog: MatDialog, private store: Store<fromStore.CartState>) {}

  items$: Observable<ICartCatalogItem[]>;
  caterings$: Observable<ICartCatering[]>;
  comments$: Observable<string>;
  customCatering$: Observable<ICatering>;
  delivery$: Observable<DeliveryAddress>;
  discount$: Observable<IDiscount>;
  cuit$: Observable<string>;
  pickupOptions$: Observable<IPickupOption[]>;
  preparationTime$: Observable<number>;
  pickup$: Observable<IPickupDate>;
  total$: Observable<number>;

  @HostBinding('@moveIn') role = '';
  @ViewChild('stepper') stepper: MatStepper;

  ngOnInit() {
    this.store.dispatch(new fromStore.LoadCaterings());
    this.caterings$ = this.store.pipe(select(fromStore.getCaterings));

    this.store.dispatch(new fromStore.LoadComments());
    this.comments$ = this.store.pipe(select(fromStore.getComments));

    this.store.dispatch(new fromStore.LoadSessionCatering());
    this.customCatering$ = this.store.pipe(select(fromStore.getCustomCatering));

    this.store.dispatch(new fromStore.GetDelivery());
    this.delivery$ = this.store.pipe(select(fromStore.getDelivery));

    this.store.dispatch(new fromStore.GetDiscount());
    this.discount$ = this.store.pipe(select(fromStore.getDiscounts));

    this.store.dispatch(new fromStore.GetCuit());
    this.cuit$ = this.store.pipe(select(fromStore.getCuit));

    this.store.dispatch(new fromStore.LoadItems());
    this.items$ = this.store.pipe(select(fromStore.getItems));

    this.store.dispatch(new fromStore.LoadPickupOptions());
    this.store.dispatch(new fromStore.GetPickupOption());
    this.store.dispatch(new fromStore.GetPreparationTime());
    this.pickupOptions$ = this.store.pipe(select(fromStore.getPickupOptions));
    this.preparationTime$ = this.store.pipe(select(fromStore.getPreparationTime));
    this.pickup$ = this.store.pipe(select(fromStore.getPickup));

    this.store.dispatch(new fromStore.GetTotals());
    this.total$ = this.store.pipe(select(fromStore.getTotal));
  }

  ngOnDestroy() {
  }

  _onNext() {
    this.stepper.next();
  }

  addCatering = (cateringId: number): void => this.store.dispatch(new fromStore.AddCatering(cateringId));
  removeCatering = (cateringId: number): void => this.store.dispatch(new fromStore.RemoveCatering(cateringId));
  
  addComments = (comments: string): void => this.store.dispatch(new fromStore.AddComments(comments));

  removeCustomCatering = (): void => this.store.dispatch(new fromStore.RemoveSessionCatering());

  clearDelivery = (): void => this.store.dispatch(new fromStore.ClearDelivery());
  updateInstructions = (address: DeliveryAddress): void => this.store.dispatch(new fromStore.UpdateInstructions(address));
  saveDelivery = (address: DeliveryAddress): void => this.store.dispatch(new fromStore.SaveDelivery(address));

  applyDiscount = (code: string): void => this.store.dispatch(new fromStore.ApplyDiscount(code));
  clearDiscount = (): void => this.store.dispatch(new fromStore.ClearDiscount());

  addCuit = (cuit: string): void => this.store.dispatch(new fromStore.AddCuit(cuit));
  clearCuit = (): void => this.store.dispatch(new fromStore.ClearCuit());
  
  addItem = (itemId: number): void => this.store.dispatch(new fromStore.AddItem(itemId));
  removeItem = (itemId: number): void => this.store.dispatch(new fromStore.RemoveItem(itemId));
  
  setPickupOption = (ticks: string): void => this.store.dispatch(new fromStore.SetPickupOption(ticks));
}
