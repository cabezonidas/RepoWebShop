import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICatering } from '../../interfaces/ICatering';
import { IItem } from '../../../products/interfaces/iitem';

@Component({
  selector: 'app-selected-items-table',
  templateUrl: './selected-items-table.component.html',
  styleUrls: ['./selected-items-table.component.scss']
})

export class SelectedItemsTableComponent implements OnInit {
  @Input() catering: ICatering;
  @Input() items: IItem[];
  @Output() addItem = new EventEmitter<number>();
  @Output() removeItem = new EventEmitter<number>();
  constructor() { }

  ngOnInit() {
  }

  onIncrement(productId: number) {
    this.addItem.emit(productId);
  }

  onDecrement(productId: number) {
    this.removeItem.emit(productId);
  }
}
