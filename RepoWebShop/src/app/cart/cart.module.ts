import { NgModule } from '@angular/core';
import { CartComponent } from './components/cart/cart.component';
import { MaterialModule } from '../material/material.module';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { DeliveryComponent } from './components/delivery/delivery.component';
import { AgmCoreModule } from '@agm/core';
import { PickupOptionsComponent } from './components/pickup/components/pickup-options/pickup-options.component';
import { PickupOptionsShellComponent } from './components/pickup/containers/pickup-options-shell/pickup-options-shell.component';
import { reducers, effects } from './store';
import { DiscountComponent } from './components/discount/discount.component';
import { TotalsComponent } from './components/totals/totals.component';
import { ProductsComponent } from './components/products/products.component';
import { InvoiceComponent } from './components/invoice/invoice.component';
import { CommentsComponent } from './components/comments/comments.component';
import { ReservationComponent } from './components/reservation/reservation.component';
import { MobileGuardService } from '../core/services/guard/mobile-guard.service';
import { PaymentComponent } from './components/payment/payment.component';
import { environment } from '../../environments/environment';

const cartRoutes: Routes = [
    { path: 'cart', component: CartComponent },
    { path: 'reservation', component: ReservationComponent, canActivate: [MobileGuardService] }
  ];

@NgModule({
    declarations: [
        CartComponent,
        DeliveryComponent,
        PickupOptionsComponent,
        PickupOptionsShellComponent,
        DiscountComponent,
        TotalsComponent,
        ProductsComponent,
        InvoiceComponent,
        CommentsComponent,
        ReservationComponent,
        PaymentComponent
    ],
    imports: [
        AgmCoreModule.forRoot({
            'apiKey': environment.googleMapsApiKey,
            'libraries': [ 'places' ]
        }),
        SharedModule,
        CommonModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forChild(cartRoutes),
        StoreModule.forFeature('cart', reducers),
        EffectsModule.forFeature(effects)
    ]
  })

export class CartModule { }
