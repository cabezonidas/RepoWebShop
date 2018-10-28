import { NgModule, LOCALE_ID } from '@angular/core';
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
import argLocale from '@angular/common/locales/es-AR';
import { registerLocaleData } from '@angular/common';
import { FbShareComponent } from './components/fb-share/fb-share.component';
import { LoadingBlockerComponent } from './components/loading-blocker/loading-blocker.component';

registerLocaleData(argLocale);

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
    ProductPriceComponent,
    FbShareComponent,
    LoadingBlockerComponent
  ],
  declarations: [LogoHeaderComponent, ProductSubtotalComponent, LoadingComponent, SnackbarComponent,
    TrolleyIconComponent, TrolleyIconShellComponent, TotalComponent, AdminGearComponent, ProductPriceComponent,
    FbShareComponent, LoadingBlockerComponent],
  // providers: [
  //   { provide: LOCALE_ID, useValue: 'es-AR"' },
  // ]
})
export class SharedModule { }
