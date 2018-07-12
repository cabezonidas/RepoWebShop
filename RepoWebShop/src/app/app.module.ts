import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material';
import { NavBarComponent } from './components/shared/nav-bar/nav-bar.component';
import { ProductPreviewComponent } from './components/shared/product-preview/product-preview.component';
import { GridComponent } from './components/products/grid/grid.component';
import { HttpClientModule } from '@angular/common/http';
import { DetailsComponent } from './components/products/details/details.component';
import { ChooseItemComponent } from './components/products/choose-item/choose-item.component';
import { CateringComponent } from './components/catering/catering.component';
import { CartComponent } from './components/shared/cart/cart.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    ProductPreviewComponent,
    GridComponent,
    CateringComponent,
    DetailsComponent,
    ChooseItemComponent,
    CartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule
  ],
  providers: [],
  entryComponents: [ChooseItemComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
