import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from './components/loading/loading.component';
import { MaterialModule } from '../material/material.module';
import { SnackbarComponent } from './components/snackbar/snackbar.component';
import { TrolleyIconComponent } from './components/trolley-icon/trolley-icon.component';
import { TrolleyIconShellComponent } from './containers/trolley-icon-shell/trolley-icon-shell.component';
import { MomentModule } from 'ngx-moment';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    MomentModule
  ],
  exports: [
    MomentModule,
    LoadingComponent,
    SnackbarComponent,
    TrolleyIconComponent,
    TrolleyIconShellComponent
  ],
  declarations: [LoadingComponent, SnackbarComponent, TrolleyIconComponent, TrolleyIconShellComponent]
})
export class SharedModule { }
