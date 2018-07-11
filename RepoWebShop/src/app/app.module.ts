import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { ProductPreviewComponent } from './product-preview/product-preview.component';
import { ProductsComponent } from './products/products.component';
import { CateringComponent } from './catering/catering.component';
import { MvcHomeComponent } from './mvc-home/mvc-home.component';
import { HttpClientModule } from '@angular/common/http';
import { DetailsComponent } from './details/details.component';
import { ChooseItemComponent } from './choose-item/choose-item.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    ProductPreviewComponent,
    ProductsComponent,
    CateringComponent,
    MvcHomeComponent,
    DetailsComponent,
    ChooseItemComponent
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
