import { Component, Input, OnInit, OnChanges, OnDestroy } from '@angular/core';
import { ICartCatalogItem } from './interfaces/icart-catalog-item';
import { CartService } from './services/cart.service';
import { AuthService } from './services/auth.service';
import { RouterStateSnapshot } from '../../node_modules/@angular/router';
import { Subscription } from '../../node_modules/rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'app';

  constructor(private cart: CartService, private auth: AuthService) { }

  userSubscription: Subscription;
  itemsSubscription: Subscription;

  products$: Array<ICartCatalogItem>;
  itemsLength$: number;

  ngOnInit() {
    this.userSubscription = this.auth.loadUser().subscribe(user => this.auth.setUser(user));
    this.itemsSubscription = this.cart.getProducts().subscribe(products => {
      this.products$ = products;
      this.itemsLength$ = 0;
      this.products$.forEach(x => this.itemsLength$ += x.amount);
      this.cart.setCartItems(products);
    });
  }

  ngOnDestroy(): void {
    this.userSubscription.unsubscribe();
    this.itemsSubscription.unsubscribe();
  }
}
