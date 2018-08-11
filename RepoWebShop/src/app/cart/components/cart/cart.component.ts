import { Component, OnInit, OnDestroy, HostBinding, ViewChild, EventEmitter, Output } from '@angular/core';
import { MatDialog, MatStepper } from '@angular/material';
import { Subscription, Observable } from 'rxjs';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';
import { moveIn, fallIn } from '../../../animations/router.animations';
import { Store, select } from '@ngrx/store';
import * as fromStore from '../../store';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
  animations: [moveIn(), fallIn()]
})

export class CartComponent implements OnInit, OnDestroy {

  constructor(public dialog: MatDialog, private store: Store<fromStore.CartState>) {}

  products$: Observable<ICartCatalogItem[]>;

  @HostBinding('@moveIn') role = '';
  @ViewChild('stepper') stepper: MatStepper;

  ngOnInit() {
    this.store.dispatch(new fromStore.LoadItems());
    this.products$ = this.store.pipe(select(fromStore.getItems));

  }
  openDialog(item: ICartCatalogItem): void {
    // const dialogRef = this.dialog.open(CartItemEditComponent, {
    //   data: item
    // });

    // dialogRef.afterClosed().subscribe(result => {
    //   console.log('The dialog was closed');
    // });
  }
  ngOnDestroy() {
  }

  _onNext() {
    this.stepper.next();
  }
}
