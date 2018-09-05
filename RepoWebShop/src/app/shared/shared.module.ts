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

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    MomentModule,
    StoreRouterModule
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
    StoreRouterModule
  ],
  declarations: [LogoHeaderComponent, ProductSubtotalComponent, LoadingComponent, SnackbarComponent,
    TrolleyIconComponent, TrolleyIconShellComponent, TotalComponent]
})
export class SharedModule { }
