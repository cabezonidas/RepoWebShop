import { NgModule } from '@angular/core';
import { CartComponent } from './components/cart/cart.component';
import { CartItemEditComponent } from './components/cart-item-edit/cart-item-edit.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { MaterialModule } from '../material/material.module';
import { CartService } from './services/cart.service';

@NgModule({
    declarations: [
        CartComponent,
        CartItemEditComponent,
        CheckoutComponent
    ],
    imports: [ MaterialModule ],
    providers: [ CartService ]
  })
export class AuthenticationModule { }
