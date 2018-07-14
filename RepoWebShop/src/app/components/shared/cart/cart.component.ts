import { Component, OnInit, Input } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { ICartCatalogItem } from '../../../interfaces/icart-catalog-item';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  constructor(private cart: CartService) { }

  products$: Array<ICartCatalogItem>;

  ngOnInit() {
    this.cart.currentProducts.subscribe(products => this.products$ = products);
  }
}
