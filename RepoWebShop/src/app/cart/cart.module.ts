import { NgModule } from '@angular/core';
import { CartComponent } from './components/cart/cart.component';
import { CartItemEditComponent } from './components/cart-item-edit/cart-item-edit.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { MaterialModule } from '../material/material.module';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

const cartRoutes: Routes = [
    { path: 'cart', component: CartComponent }
  ];

@NgModule({
    declarations: [
        CartComponent,
        CartItemEditComponent,
        CheckoutComponent
    ],
    imports: [
        SharedModule,
        CommonModule,
        MaterialModule,
        ReactiveFormsModule,
        RouterModule.forChild(cartRoutes),
        // StoreModule.forFeature('cart', reducer),
        // EffectsModule.forFeature([CartEffects])
    ]
  })

export class CartModule { }
