import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { CateringComponent } from './catering/catering.component';
import { DetailsComponent } from './details/details.component';
import { MvcHomeComponent } from './mvc-home/mvc-home.component';

const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: 'mvc-home', component: MvcHomeComponent },
  { path: 'catering', component: CateringComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'products/:id', component: DetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
