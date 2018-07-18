import { Component, OnInit, Inject } from '@angular/core';
import { ICartCatalogItem } from '../../../interfaces/icart-catalog-item';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { CartService } from '../../../services/cart.service';

@Component({
  selector: 'app-cart-item-edit',
  templateUrl: './cart-item-edit.component.html',
  styleUrls: ['./cart-item-edit.component.scss']
})
export class CartItemEditComponent implements OnInit {

  public amount: number;

  constructor(
    public dialogRef: MatDialogRef<CartItemEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ICartCatalogItem,
    private cart: CartService
  ) {}


  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
    this.cart.currentProducts.subscribe(products => {
      const cartItem = products.find(x => x.product.productId === this.data.product.productId);
      this.data.amount = cartItem ? cartItem.amount : 0;
    });
  }

  addToCart() {
    this.cart.addProductToCart(this.data.product.productId);
  }
  removeFromCart() {
    this.cart.removeProductFromCart(this.data.product.productId);
  }

}
