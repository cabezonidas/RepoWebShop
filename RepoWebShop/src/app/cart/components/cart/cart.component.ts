import { Component, OnInit, OnDestroy } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { MatDialog } from '@angular/material';
import { CartItemEditComponent } from '../cart-item-edit/cart-item-edit.component';
import { Subscription } from 'rxjs';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})

export class CartComponent implements OnInit, OnDestroy {

  constructor(public dialog: MatDialog) {}

  ngOnInit() {

  }
  openDialog(item: ICartCatalogItem): void {
    const dialogRef = this.dialog.open(CartItemEditComponent, {
      data: item
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
  ngOnDestroy() {
  }
}
