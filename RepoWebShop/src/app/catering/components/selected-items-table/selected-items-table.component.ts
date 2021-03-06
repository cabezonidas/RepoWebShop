import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { ICatering } from '../../interfaces/ICatering';
import { IItem } from '../../../products/interfaces/iitem';
import { CateringService } from '../../services/catering.service';

@Component({
  selector: 'app-selected-items-table',
  templateUrl: './selected-items-table.component.html',
  styleUrls: ['./selected-items-table.component.scss']
})

export class SelectedItemsTableComponent implements OnInit, OnChanges {
  @Input() catering: ICatering;
  @Input() items: IItem[];
  @Output() addItem = new EventEmitter<number>();
  @Output() removeItem = new EventEmitter<number>();
  subtotal = 0;
  subtotalInStore = 0;

  constructor(private cateringService: CateringService) {}

  ngOnInit() {}

  ngOnChanges() {
    this.subtotal = this.cateringService.totalOnline(this.catering);
    this.subtotalInStore = this.cateringService.totalInStore(this.catering);
  }
  onIncrement(productId: number) {
    this.addItem.emit(productId);
  }

  onDecrement(productId: number) {
    this.removeItem.emit(productId);
  }
}
