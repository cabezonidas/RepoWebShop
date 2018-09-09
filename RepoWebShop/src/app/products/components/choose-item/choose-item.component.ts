import { Component, OnInit, Inject } from '@angular/core';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material';
import { IItem } from '../../interfaces/iitem';
import * as fromProduct from '../../state';
import * as fromStore from '../../../cart/store';
import { Store } from '@ngrx/store';
import { CalendarService } from '../../../home/services/calendar.service';
@Component({
  selector: 'app-choose-item',
  templateUrl: './choose-item.component.html',
  styleUrls: ['./choose-item.component.scss']
})

export class ChooseItemComponent implements OnInit {

  constructor(
    @Inject(MAT_BOTTOM_SHEET_DATA) public items$: Array<IItem>,
    public bottomSheetRef: MatBottomSheetRef<ChooseItemComponent>,
    private store: Store<fromProduct.State>,
    private calendar: CalendarService
  ) {}

  addToCart(id: number): void {
    this.bottomSheetRef.dismiss();
    this.store.dispatch(new fromStore.AddItem(id));
  }

  ngOnInit() {
  }

  soonestPickup = (date: Date) => this.calendar.soonestPickup(new Date(date));
}
