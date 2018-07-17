import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { ICartCatalogItem } from './interfaces/icart-catalog-item';
import { CartService } from './services/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(private cart: CartService) { }

  products$: Array<ICartCatalogItem>;
  itemsLength$: number;

  ngOnInit() {
    this.cart.currentProducts.subscribe(products => {
      this.products$ = products;
      this.itemsLength$ = 0;
      this.products$.forEach(x => this.itemsLength$ += x.amount);
    });
    this.cart.getProducts();
  }
}
