import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewCateringShellComponent } from './containers/new-catering-shell/new-catering-shell.component';
import { Routes, RouterModule } from '@angular/router';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { reducer } from './state/catering.reducer';
import { CateringEffects } from './state/catering.effects';
import { MaterialModule } from '../material/material.module';
import { ItemsTableComponent } from './components/items-table/items-table.component';
import { SelectedItemsTableComponent } from './components/selected-items-table/selected-items-table.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AutocompleteItemsComponent } from './components/autocomplete-items/autocomplete-items.component';

const cateringRoutes: Routes = [
  { path: 'new-catering', component: NewCateringShellComponent }
];

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    RouterModule.forChild(cateringRoutes),
    StoreModule.forFeature('catering', reducer),
    EffectsModule.forFeature([CateringEffects])
  ],
  declarations: [NewCateringShellComponent, ItemsTableComponent, SelectedItemsTableComponent, AutocompleteItemsComponent]
})
export class CateringModule { }
