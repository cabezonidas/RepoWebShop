import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID, Inject, PLATFORM_ID, APP_ID } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductModule } from './products/products.module';
import { SharedModule } from './shared/shared.module';
import { MaterialModule } from './material/material.module';
import { AuthenticationModule } from './authentication/authentication.module';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { EffectsModule } from '@ngrx/effects';
import { CateringModule } from './catering/catering.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule } from '@angular/material';
import { MainNavComponent } from './shared/components/main-nav/main-nav.component';
import { CartModule } from './cart/cart.module';
import { HomeModule } from './home/home.module';
import { AdminModule } from './admin/admin.module';
import { LeftnavContainerComponent } from './components/leftnav-container/leftnav-container.component';
import { ToolBarComponent } from './components/tool-bar/tool-bar.component';
import { isPlatformBrowser } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    LeftnavContainerComponent,
    MainNavComponent,
    ToolBarComponent
  ],
  imports: [
    AdminModule,
    SharedModule,
    MaterialModule,
    BrowserModule.withServerTransition({ appId: 'de-las-artes' }),
    AuthenticationModule,
    ProductModule,
    CateringModule,
    HomeModule,
    CartModule,
    AppRoutingModule,
    EffectsModule.forRoot([]),
    StoreModule.forRoot({}),
    StoreDevtoolsModule.instrument({
      name: 'De las Artes App Devtools',
      maxAge: 25,
      logOnly: environment.production
    }),
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(@Inject(PLATFORM_ID) private platformId: Object, @Inject(APP_ID) private appId: string) {
    const platform = isPlatformBrowser(platformId) ? 'in the browser' : 'on the server';
    console.log(`Running ${platform} with appId=${appId}`);
  }
}
