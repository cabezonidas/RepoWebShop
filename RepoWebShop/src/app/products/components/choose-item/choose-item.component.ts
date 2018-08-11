import { Component, OnInit, Inject } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material';
import { IItem } from '../../interfaces/iitem';

@Component({
  selector: 'app-choose-item',
  templateUrl: './choose-item.component.html',
  styleUrls: ['./choose-item.component.scss']
})

export class ChooseItemComponent implements OnInit {

  constructor(
    @Inject(MAT_BOTTOM_SHEET_DATA) public items$: Array<IItem>,
    private bottomSheetRef: MatBottomSheetRef<ChooseItemComponent>
  ) {}

  addToCart(id: MouseEvent): void {
    this.bottomSheetRef.dismiss();
    // this.cart.addProductToCart(id);
  }

  ngOnInit() {
  }
}
