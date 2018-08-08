import { Component, OnInit, OnDestroy, HostBinding, ViewChild } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { MatDialog, MatStepper } from '@angular/material';
import { Subscription } from 'rxjs';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';
import { moveIn, fallIn } from '../../../animations/router.animations';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
  animations: [moveIn(), fallIn()]
})

export class CartComponent implements OnInit, OnDestroy {

  constructor(public dialog: MatDialog) {}

  @HostBinding('@moveIn') role = '';
  @ViewChild('stepper') stepper: MatStepper;

  ngOnInit() {

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
}
