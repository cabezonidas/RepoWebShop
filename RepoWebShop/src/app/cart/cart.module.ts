import { NgModule } from '@angular/core';
import { CartComponent } from './components/cart/cart.component';
import { MaterialModule } from '../material/material.module';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { DeliveryComponent } from './components/delivery/delivery.component';
import { AgmCoreModule } from '@agm/core';

const cartRoutes: Routes = [
    { path: 'cart', component: CartComponent }
  ];

@NgModule({
    declarations: [
        CartComponent,
        DeliveryComponent
    ],
    imports: [
        AgmCoreModule.forRoot({
            'apiKey': 'AIzaSyAR5nmTSuiZsjA5Yhgx3w9EDEyF-C8zIwU',
            'libraries': [ 'places' ]
        }),
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
