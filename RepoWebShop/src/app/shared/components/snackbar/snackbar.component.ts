import { Component, OnInit, OnDestroy } from '@angular/core';
import {MatSnackBar, MatSnackBarRef} from '@angular/material';
import { Store } from '@ngrx/store';
import * as fromEffects from '../../../cart/store/effects';
import * as fromCateringEffects from '../../../catering/state/catering.effects';
import * as fromCateringState from '../../../catering/state/catering.actions';
import * as fromStore from '../../../cart/store';
import { filter } from 'rxjs/operators';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-snackbar',
  templateUrl: './snackbar.component.html',
  styleUrls: ['./snackbar.component.scss']
})
export class SnackbarComponent implements OnInit, OnDestroy {

  snackBarRef: MatSnackBarRef<any>;

  itemAddedSub = new Subscription();
  itemRemoveSub = new Subscription();
  cateringAddedSub = new Subscription();
  cateringRemovedSub = new Subscription();
  cateringSavedSub = new Subscription();

  constructor(
    private store: Store<fromStore.CartState>,
    private router: Router,
    public snackBar: MatSnackBar,
    private itemEffects: fromEffects.ItemsEffects,
    private cartCateringEffects: fromEffects.CateringsEffects,
    private cateringEffects: fromCateringEffects.CateringEffects
  ) { }

  ngOnInit() {
    this.itemAddedSub = this.itemEffects.addItem$
      .pipe(filter(action => action.type === fromStore.ItemActionTypes.AddSuccess))
      .subscribe(() => this.openSnackBar('¡Item agregado!'));
    this.itemRemoveSub = this.itemEffects.removeItem$
      .pipe(filter(action => action.type === fromStore.ItemActionTypes.RemoveSuccess))
      .subscribe(() => this.openSnackBar('¡Item quitado!'));

    this.cateringAddedSub = this.cartCateringEffects.addCatering$
      .pipe(filter(action => action.type === fromStore.CateringActionTypes.AddCateringSuccess))
      .subscribe(() => this.openSnackBar('¡Catering agregado!'));
    this.cateringRemovedSub = this.cartCateringEffects.removeCatering$
      .pipe(filter(action => action.type === fromStore.CateringActionTypes.RemoveCateringSuccess))
      .subscribe(() => this.openSnackBar('¡Catering quitado!'));
    this.cateringSavedSub = this.cateringEffects.saveCustomCatering$
      .pipe(filter(action => action.type === fromCateringState.CateringActionTypes.SavingCustomCateringSuccess))
      .subscribe(() => this.openSnackBar('¡Catering guardado!'));
  }

  ngOnDestroy() {
    this.itemAddedSub.unsubscribe();
    this.itemRemoveSub.unsubscribe();
    this.cateringAddedSub.unsubscribe();
    this.cateringRemovedSub.unsubscribe();
    this.cateringSavedSub.unsubscribe();
  }

  openSnackBar = (msg: string) => {
    const action = this.router.url !== '/cart' ? 'Ir al carrito' : null;
    const snackBarRef = this.snackBar.open(msg, action, {duration: 3000});
    snackBarRef.onAction().subscribe(() => {
      this.router.navigateByUrl('/cart');
    });
  }

}
