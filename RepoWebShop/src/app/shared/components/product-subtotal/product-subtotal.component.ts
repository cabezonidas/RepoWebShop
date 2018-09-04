import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-product-subtotal',
  templateUrl: './product-subtotal.component.html',
  styleUrls: ['./product-subtotal.component.scss']
})
export class ProductSubtotalComponent implements OnInit {

  @Input() price: number;
  @Input() priceInStore: number;
  @Input() amount: number;

  constructor() { }

  ngOnInit() {
  }
}
