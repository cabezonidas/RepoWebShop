import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from './components/loading/loading.component';
import { MaterialModule } from '../material/material.module';
import { SnackbarComponent } from './components/snackbar/snackbar.component';
import { TrolleyIconComponent } from './components/trolley-icon/trolley-icon.component';
import { ProductSubtotalComponent } from './components/product-subtotal/product-subtotal.component';
import { TrolleyIconShellComponent } from './containers/trolley-icon-shell/trolley-icon-shell.component';
import { MomentModule } from 'ngx-moment';
import { TotalComponent } from './components/total/total.component';
import { StoreRouterModule } from '../router/store-router.module';
import { LogoHeaderComponent } from './components/logo-header/logo-header.component';
import { AdminGearComponent } from './components/admin-gear/admin-gear.component';
import { ProductPriceComponent } from './components/product-price/product-price.component';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    MomentModule,
    StoreRouterModule,
    RouterModule
  ],
  exports: [
    LogoHeaderComponent,
    MomentModule,
    LoadingComponent,
    SnackbarComponent,
    TrolleyIconComponent,
    TrolleyIconShellComponent,
    ProductSubtotalComponent,
    TotalComponent,
    StoreRouterModule,
    AdminGearComponent,
    ProductPriceComponent
  ],
  declarations: [LogoHeaderComponent, ProductSubtotalComponent, LoadingComponent, SnackbarComponent,
    TrolleyIconComponent, TrolleyIconShellComponent, TotalComponent, AdminGearComponent, ProductPriceComponent]
})
export class SharedModule { }
