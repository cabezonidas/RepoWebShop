import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import {
  StoreRouterConnectingModule,
  RouterStateSerializer,
} from '@ngrx/router-store';
import { StoreModule, MetaReducer } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { reducers, effects, CustomSerializer } from './store';

@NgModule({
    imports: [
      BrowserModule,
      BrowserAnimationsModule,
      StoreModule.forRoot(reducers),
      EffectsModule.forRoot(effects),
      StoreRouterConnectingModule,
    ],
    providers: [{ provide: RouterStateSerializer, useClass: CustomSerializer }],
  })
  export class StoreRouterModule {}
