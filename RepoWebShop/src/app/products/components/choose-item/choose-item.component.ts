import { Component, OnInit, Inject } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material';
import { IItem } from '../../interfaces/iitem';
import * as fromProduct from '../../state';
import * as fromStore from '../../../cart/store';
import { Store } from '@ngrx/store';
@Component({
  selector: 'app-choose-item',
  templateUrl: './choose-item.component.html',
  styleUrls: ['./choose-item.component.scss']
})

export class ChooseItemComponent implements OnInit {

  constructor(
    @Inject(MAT_BOTTOM_SHEET_DATA) public items$: Array<IItem>,
    private bottomSheetRef: MatBottomSheetRef<ChooseItemComponent>,
    private store: Store<fromProduct.State>
  ) {}

  addToCart(id: number): void {
    this.bottomSheetRef.dismiss();
    this.store.dispatch(new fromStore.AddItem(id));
  }

  ngOnInit() {
  }
}
