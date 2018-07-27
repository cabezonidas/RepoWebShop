import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
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
import { ProductCarouselPreviewComponent } from './components/shared/product-carousel-preview/product-carousel-preview.component';
import { FullCatalogComponent } from './components/shared/full-catalog/full-catalog.component';
import { SoonestPickupComponent } from './components/shared/soonest-pickup/soonest-pickup.component';
import { PriceComparisonComponent } from './components/shared/price-comparison/price-comparison.component';
import { CartItemEditComponent } from './components/shared/cart-item-edit/cart-item-edit.component';
import { LoginComponent } from './components/account/login/login.component';
import { EmailComponent } from './components/account/email/email.component';
import { SignupComponent } from './components/account/signup/signup.component';
import { MembersComponent } from './components/account/members/members.component';

import { AngularFireModule } from 'angularfire2';
import { AngularFireAuthModule } from 'angularfire2/auth';
import { environment } from '../environments/environment';
import { AuthGuardService } from './services/auth-guard.service';
import { AdminGuardService } from './services/admin-guard.service';
import { MobileComponent } from './components/account/mobile/mobile.component';
import { PasswordComponent } from './components/account/password/password.component';
import { CheckoutComponent } from './components/cart/checkout/checkout.component';
import { MobileGuardService } from './services/mobile-guard.service';

@NgModule({
  declarations: [
    AppComponent, NavBarComponent, ProductPreviewComponent, GridComponent, CateringComponent, DetailsComponent, ChooseItemComponent,
    CartComponent, ProductCarouselPreviewComponent, FullCatalogComponent, SoonestPickupComponent, PriceComparisonComponent,
    CartItemEditComponent, LoginComponent, EmailComponent, SignupComponent, MembersComponent, MobileComponent, PasswordComponent,
    CheckoutComponent
  ],
  imports: [
    AngularFireModule.initializeApp(environment.firebase), AngularFireAuthModule, BrowserModule, AppRoutingModule, BrowserAnimationsModule,
    MaterialModule, HttpClientModule, FormsModule, ReactiveFormsModule
  ],
  providers: [ AuthGuardService, AdminGuardService, MobileGuardService ],
  entryComponents: [ChooseItemComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
