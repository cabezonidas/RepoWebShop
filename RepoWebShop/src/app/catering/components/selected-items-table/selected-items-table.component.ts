import { Component, OnInit, Input } from '@angular/core';
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
  constructor() { }

  ngOnInit() {
  }

  getItem = (id: number): IItem => this.items.find(i => i.productId === id);

}
