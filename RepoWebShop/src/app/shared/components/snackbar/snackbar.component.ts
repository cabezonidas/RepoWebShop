import { Component, OnInit, OnDestroy } from '@angular/core';
import {MatSnackBar, MatSnackBarRef} from '@angular/material';
import { Store } from '@ngrx/store';
import * as fromEffects from '../../../cart/store/effects';
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
  snackBarRefSub = new Subscription();

  itemAddedSub = new Subscription();
  constructor(
    private router: Router,
    public snackBar: MatSnackBar,
    private itemEffects: fromEffects.ItemsEffects
  ) { }

  ngOnInit() {
    this.itemAddedSub = this.itemEffects.addItem$.pipe(filter(action => action.type === fromStore.ItemActionTypes.AddSuccess))
      .subscribe(() => this.openSnackBar('Item agregado!'));
  }

  ngOnDestroy() {
    this.snackBarRefSub.unsubscribe();
    this.itemAddedSub.unsubscribe();
  }

  openSnackBar = (msg: string) => {
    const action = this.router.url !== '/cart' ? 'Ir al carrito' : null;
    this.snackBarRef = this.snackBar.open(msg, action, {duration: 3000});
    this.snackBarRef.onAction().subscribe(() => {
      this.router.navigateByUrl('/cart');
    });
  }

}
