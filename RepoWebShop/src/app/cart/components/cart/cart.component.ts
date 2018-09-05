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
  cuitDetails$: Observable<string[]>;
  pickupOptions$: Observable<IPickupOption[]>;
  preparationTime$: Observable<number>;
  pickup$: Observable<IPickupDate>;
  total$: Observable<number>;
  totalGoods$: Observable<number>;
  totalGoodsInStore$: Observable<number>;
  totalInStore$: Observable<number>;
  totalWithoutDiscount$: Observable<number>;
  totalItems$: Observable<number>;
  totalItemsInStore$: Observable<number>;
  totalCaterings$: Observable<number>;
  totalCateringsInStore$: Observable<number>;
  totalCustomCatering$: Observable<number>;
  totalCustomCateringInStore$: Observable<number>;

  cateringsLoaded$: Observable<boolean>;
  cateringsLoading$: Observable<boolean>;
  commentsLoaded$: Observable<boolean>;
  commentsLoading$: Observable<boolean>;
  customCateringLoaded$: Observable<boolean>;
  customCateringLoading$: Observable<boolean>;
  deliveryLoaded$: Observable<boolean>;
  deliveryLoading$: Observable<boolean>;
  discountsLoaded$: Observable<boolean>;
  discountsLoading$: Observable<boolean>;
  cuitLoaded$: Observable<boolean>;
  cuitLoading$: Observable<boolean>;
  itemsLoaded$: Observable<boolean>;
  itemsLoading$: Observable<boolean>;
  pickupLoaded$: Observable<boolean>;
  pickupLoading$: Observable<boolean>;
  totalsLoaded$: Observable<boolean>;
  totalsLoading$: Observable<boolean>;

  @HostBinding('@moveIn') role = '';
  @ViewChild('stepper') stepper: MatStepper;

  ngOnInit() {
    this.store.dispatch(new fromStore.LoadCaterings());
    this.caterings$ = this.store.pipe(select(fromStore.getCaterings));
    this.cateringsLoaded$ = this.store.pipe(select(fromStore.getCateringsLoaded));
    this.cateringsLoading$ = this.store.pipe(select(fromStore.getCateringsLoading));

    this.store.dispatch(new fromStore.LoadComments());
    this.comments$ = this.store.pipe(select(fromStore.getComments));
    this.commentsLoaded$ = this.store.pipe(select(fromStore.getCommentsLoaded));
    this.commentsLoading$ = this.store.pipe(select(fromStore.getCommentsLoading));

    this.store.dispatch(new fromStore.LoadSessionCatering());
    this.customCatering$ = this.store.pipe(select(fromStore.getCustomCatering));
    this.customCateringLoaded$ = this.store.pipe(select(fromStore.getCustomCateringLoaded));
    this.customCateringLoading$ = this.store.pipe(select(fromStore.getCustomCateringLoading));


    this.store.dispatch(new fromStore.GetDelivery());
    this.delivery$ = this.store.pipe(select(fromStore.getDelivery));
    this.deliveryLoaded$ = this.store.pipe(select(fromStore.getDeliveryLoaded));
    this.deliveryLoading$ = this.store.pipe(select(fromStore.getDeliveryLoading));

    this.store.dispatch(new fromStore.GetDiscount());
    this.discount$ = this.store.pipe(select(fromStore.getDiscounts));
    this.discountsLoaded$ = this.store.pipe(select(fromStore.getDiscountsLoaded));
    this.discountsLoading$ = this.store.pipe(select(fromStore.getDiscountsLoading));

    this.store.dispatch(new fromStore.GetCuit());
    this.cuit$ = this.store.pipe(select(fromStore.getCuit));
    this.cuitLoaded$ = this.store.pipe(select(fromStore.getInvoiceLoaded));
    this.cuitLoading$ = this.store.pipe(select(fromStore.getInvoicesLoading));

    this.store.dispatch(new fromStore.LoadItems());
    this.items$ = this.store.pipe(select(fromStore.getItems));
    this.itemsLoaded$ = this.store.pipe(select(fromStore.getItemsLoaded));
    this.itemsLoading$ = this.store.pipe(select(fromStore.getItemsLoading));

    this.store.dispatch(new fromStore.LoadPickupOptions());
    this.store.dispatch(new fromStore.GetPickupOption());
    this.store.dispatch(new fromStore.GetPreparationTime());
    this.pickupOptions$ = this.store.pipe(select(fromStore.getPickupOptions));
    this.preparationTime$ = this.store.pipe(select(fromStore.getPreparationTime));
    this.pickup$ = this.store.pipe(select(fromStore.getPickup));
    this.pickupLoaded$ = this.store.pipe(select(fromStore.getPickupLoaded));
    this.pickupLoading$ = this.store.pipe(select(fromStore.getPickupLoading));

    this.store.dispatch(new fromStore.GetTotals());
    this.total$ = this.store.pipe(select(fromStore.getTotal));
    this.totalGoods$ = this.store.pipe(select(fromStore.getTotalGoods));
    this.totalGoodsInStore$ = this.store.pipe(select(fromStore.getTotalGoodsInStore));
    this.totalInStore$ = this.store.pipe(select(fromStore.getTotalInStore));
    this.totalWithoutDiscount$ = this.store.pipe(select(fromStore.getTotalWithoutDiscount));
    this.totalItems$ = this.store.pipe(select(fromStore.getTotalItems));
    this.totalItemsInStore$ = this.store.pipe(select(fromStore.getTotalItemsInStore));
    this.totalCaterings$ = this.store.pipe(select(fromStore.getTotalCaterings));
    this.totalCateringsInStore$ = this.store.pipe(select(fromStore.getTotalCateringsInStore));
    this.totalCustomCatering$ = this.store.pipe(select(fromStore.getTotalCustomCatering));
    this.totalCustomCateringInStore$ = this.store.pipe(select(fromStore.getTotalCustomCateringInStore));
    this.totalsLoaded$ = this.store.pipe(select(fromStore.getTotalsLoaded));
    this.totalsLoading$ = this.store.pipe(select(fromStore.getTotalsLoading));
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
