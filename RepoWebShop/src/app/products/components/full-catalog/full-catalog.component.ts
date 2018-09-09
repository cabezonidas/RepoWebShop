import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { IItem } from '../../interfaces/iitem';
import { IProduct } from '../../interfaces/iproduct';
import { MatTableDataSource } from '@angular/material';
import { Store } from '@ngrx/store';
import * as fromProduct from '../../state';
import * as fromStore from '../../../cart/store';
import { CalendarService } from '../../../home/services/calendar.service';

@Component({
  selector: 'app-full-catalog',
  templateUrl: './full-catalog.component.html',
  styleUrls: ['./full-catalog.component.scss']
})
export class FullCatalogComponent implements OnInit, OnChanges {

  @Input() products: Array<IProduct>;

  dataSource: MatTableDataSource<IItem>;
  items: Array<IItem>;

  displayedColumns: string[] = [
    'displayName',
    // 'action'
  ];

  constructor(private store: Store<fromProduct.State>, private calendar: CalendarService) {}

  ngOnInit() {
    this.feedTable();
  }
  ngOnChanges() {
    this.feedTable();
  }

  feedTable = () => {
    this.items = [];
    this.products.forEach(x => x.items.forEach(y => this.items.push(y)));
    this.dataSource = new MatTableDataSource(this.items);
  }

  addToCart(id: number): void {
    this.store.dispatch(new fromStore.AddItem(id));
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  soonestPickup = (date: Date) => this.calendar.soonestPickup(new Date(date));
}
