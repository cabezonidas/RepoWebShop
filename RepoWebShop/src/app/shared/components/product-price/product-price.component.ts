import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-product-price',
  templateUrl: './product-price.component.html',
  styleUrls: ['./product-price.component.scss']
})
export class ProductPriceComponent implements OnInit {

  @Input() price: number;
  @Input() priceInStore: number;

  constructor() { }

  ngOnInit() {
  }

}
