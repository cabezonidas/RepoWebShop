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

const adminRoutes: Routes = [
{
    path: 'settings',
    component: TopBarComponent,
    canActivate: [AdminGuardService],
    children: [
        { path: '', redirectTo: 'active-orders', pathMatch: 'full' },
        { path: 'active-orders', component: ActiveOrdersShellComponent },
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
    TopBarComponent, OrdersShellComponent, ActiveOrdersShellComponent, OrderDialogShellComponent, OrderDetailsComponent
    ],
  exports: [
    ],
  entryComponents: [
    OrderDialogShellComponent
  ],
})
export class AdminModule { }
