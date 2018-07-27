import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GridComponent } from './components/products/grid/grid.component';
import { DetailsComponent } from './components/products/details/details.component';
import { CartComponent } from './components/shared/cart/cart.component';
import { LoginComponent } from './components/account/login/login.component';
import { SignupComponent } from './components/account/signup/signup.component';
import { EmailComponent } from './components/account/email/email.component';
import { MembersComponent } from './components/account/members/members.component';
import { AuthGuardService } from './services/auth-guard.service';
import { AdminGuardService } from './services/admin-guard.service';
import { MobileComponent } from './components/account/mobile/mobile.component';
import { PasswordComponent } from './components/account/password/password.component';
import { CheckoutComponent } from './components/cart/checkout/checkout.component';
import { MobileGuardService } from './services/mobile-guard.service';



const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: 'products', component: GridComponent },
  { path: 'mobile', component: MobileComponent, canActivate: [AuthGuardService] },
  { path: 'cart', component: CartComponent },
  { path: 'products/:id', component: DetailsComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'login-email', component: EmailComponent },
  { path: 'reset-password', component: PasswordComponent },
  { path: 'checkout', component: CheckoutComponent, canActivate: [MobileGuardService] },
  { path: 'profile', component: MembersComponent, canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

// export const routes: ModuleWithProviders = RouterModule.forRoot(router);


