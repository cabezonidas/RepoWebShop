import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChooseItemComponent } from './components/choose-item/choose-item.component';
import { DetailsComponent } from './components/details/details.component';
import { FullCatalogComponent } from './components/full-catalog/full-catalog.component';
import { PriceComparisonComponent } from './components/price-comparison/price-comparison.component';
import { ProductCarouselPreviewComponent } from './components/product-carousel-preview/product-carousel-preview.component';
import { ProductPreviewComponent } from './components/product-preview/product-preview.component';
import { SoonestPickupComponent } from './components/soonest-pickup/soonest-pickup.component';
import { GridComponent } from './components/grid/grid.component';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { MaterialModule } from '../material/material.module';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { reducer } from './state/product.reducer';
import { ProductEffects } from './state/product.effects';

const productRoutes: Routes = [
  { path: 'products', component: GridComponent },
  { path: 'products/:id', component: DetailsComponent }
];

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    SharedModule,
    RouterModule.forChild(productRoutes),
    StoreModule.forFeature('product', reducer),
    // StoreModule.forFeature('products', {
    //  productList: listReducer,
    //  productData: dataReducer // (images)
    // })
    EffectsModule.forFeature([ProductEffects])
  ],
  declarations: [
    ChooseItemComponent, DetailsComponent, FullCatalogComponent, GridComponent,
    PriceComparisonComponent, ProductCarouselPreviewComponent, ProductPreviewComponent, SoonestPickupComponent
  ],
  entryComponents: [ChooseItemComponent],
})
export class ProductModule { }