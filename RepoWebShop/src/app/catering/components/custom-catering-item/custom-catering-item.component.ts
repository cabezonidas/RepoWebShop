import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ICateringItem } from '../../interfaces/ICateringItem';
import { IItem } from 'src/app/products/interfaces/iitem';

@Component({
  selector: 'app-custom-catering-item',
  templateUrl: './custom-catering-item.component.html',
  styleUrls: ['./custom-catering-item.component.scss']
})
export class CustomCateringItemComponent implements OnInit {

  @Input() items: ICateringItem[];
  @Output() Increment = new EventEmitter<IItem>();
  @Output() Decrement = new EventEmitter<IItem>();

  constructor() { }

  ngOnInit() {
  }

}
