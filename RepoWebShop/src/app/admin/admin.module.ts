import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../material/material.module';
import { TopBarComponent } from './components/top-bar/top-bar.component';
import { AdminGuardService } from '../core/services/guard/admin-guard.service';
import { ActiveOrdersShellComponent } from './containers/active-orders-shell/active-orders-shell.component';
import { OrdersShellComponent } from './containers/orders-shell/orders-shell.component';
import { OrderDialogShellComponent } from './containers/order-dialog-shell/order-dialog-shell.component';
import { OrderDetailsComponent } from './components/order-details/order-details.component';
import { OrderSummaryComponent } from './components/order-summary/order-summary.component';
import { OrderDeliveryComponent } from './components/order-delivery/order-delivery.component';
import { AllOrdersShellComponent } from './containers/all-orders-shell/all-orders-shell.component';
import { OrderCustomerComponent } from './components/order-customer/order-customer.component';
import { OrderBillingComponent } from './components/order-billing/order-billing.component';

const adminRoutes: Routes = [
{
    path: 'settings',
    component: TopBarComponent,
    canActivate: [AdminGuardService],
    children: [
        { path: '', redirectTo: 'active-orders', pathMatch: 'full' },
        { path: 'active-orders', component: ActiveOrdersShellComponent },
        { path: 'all-orders', component: AllOrdersShellComponent },
        { path: '**', redirectTo: 'active-orders', pathMatch: 'full' }
    ]
}];

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    SharedModule,
    RouterModule.forChild(adminRoutes)
    ],
  declarations: [
    TopBarComponent, OrdersShellComponent, ActiveOrdersShellComponent,
    OrderDialogShellComponent, OrderDetailsComponent, OrderSummaryComponent,
    OrderDeliveryComponent, AllOrdersShellComponent, OrderCustomerComponent, OrderBillingComponent
    ],
  exports: [
    ],
  entryComponents: [
    OrderDialogShellComponent
  ],
})
export class AdminModule { }
