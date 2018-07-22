import { Component, OnInit, Input } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { ICartCatalogItem } from '../../../interfaces/icart-catalog-item';
import { MatDialog } from '@angular/material';
import { CartItemEditComponent } from '../cart-item-edit/cart-item-edit.component';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})

export class CartComponent implements OnInit {

  constructor(private cart: CartService, public dialog: MatDialog, private auth: AuthService) {}

  products$: Array<ICartCatalogItem>;

  ngOnInit() {
    this.cart.currentProducts.subscribe(products => this.products$ = products);
    this.auth.returnUrl.subscribe(data => console.log(data));
  }
  openDialog(item: ICartCatalogItem): void {
    console.log('1');
    const dialogRef = this.dialog.open(CartItemEditComponent, {
      data: item
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      // this.animal = result;
    });
  }
}
