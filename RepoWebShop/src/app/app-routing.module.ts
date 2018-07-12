import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GridComponent } from './components/products/grid/grid.component';
import { DetailsComponent } from './components/products/details/details.component';
import { CartComponent } from './components/shared/cart/cart.component';

const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: 'products', component: GridComponent },
  { path: 'cart', component: CartComponent },
  { path: 'products/:id', component: DetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
