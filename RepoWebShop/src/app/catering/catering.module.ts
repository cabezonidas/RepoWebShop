  import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewCateringShellComponent } from './containers/new-catering-shell/new-catering-shell.component';
import { Routes, RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { reducer } from './state/catering.reducer';
import { CateringEffects } from './state/catering.effects';
import { MaterialModule } from '../material/material.module';
import { ProductModule } from '../products/products.module';
import { ItemsTableComponent } from './components/items-table/items-table.component';
import { SelectedItemsTableComponent } from './components/selected-items-table/selected-items-table.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AutocompleteItemsComponent } from './components/autocomplete-items/autocomplete-items.component';
import { NewCateringSubtotalHeaderComponent } from './components/new-catering-subtotal-header/new-catering-subtotal-header.component';
import { SharedModule } from '../shared/shared.module';
import { CateringOptionsShellComponent } from './containers/catering-options-shell/catering-options-shell.component';
import { CateringOptionComponent } from './components/catering-option/catering-option.component';
import { SlickModule } from 'ngx-slick';
import { CateringsCarouselShellComponent } from './containers/caterings-carousel-shell/caterings-carousel-shell.component';
import { CustomCateringShellComponent } from './containers/custom-catering-shell/custom-catering-shell.component';
import { CustomCateringItemComponent } from './components/custom-catering-item/custom-catering-item.component';

const cateringRoutes: Routes = [
  { path: 'new-catering', component: NewCateringShellComponent },
  { path: 'custom-catering', component: CustomCateringShellComponent },
  { path: 'catering-options', component: CateringOptionsShellComponent }
];

@NgModule({
  imports: [
    SharedModule,
    SlickModule,
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    ProductModule,
    RouterModule.forChild(cateringRoutes),
    StoreModule.forFeature('catering', reducer),
    EffectsModule.forFeature([CateringEffects]),
  ],
  declarations: [NewCateringShellComponent, ItemsTableComponent, SelectedItemsTableComponent,
    AutocompleteItemsComponent, NewCateringSubtotalHeaderComponent, CateringOptionComponent,
    CateringOptionsShellComponent, CateringsCarouselShellComponent, CustomCateringShellComponent, CustomCateringItemComponent]
})
export class CateringModule { }
