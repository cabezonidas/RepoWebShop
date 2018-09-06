import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { ICartCatalogItem } from '../../../cart/interfaces/icart-catalog-item';
import { ICartCatering } from '../../../cart/interfaces/icart-catering';
import { ICatering } from '../../../catering/interfaces/ICatering';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-trolley-icon',
  templateUrl: './trolley-icon.component.html',
  styleUrls: ['./trolley-icon.component.scss']
})
export class TrolleyIconComponent implements OnInit, OnChanges {

  @Input() items: ICartCatalogItem[];
  @Input() caterings: ICartCatering[];
  @Input() customCatering: ICatering;

  constructor(private titleService: Title) { }
  ngOnInit() {
  }
  ngOnChanges() {
    const title = this.titleService.getTitle().split('(')[0];
    if (this.count()) {
      this.titleService.setTitle(title + ' (' + this.count() + ')' );
    }
  }

  count() {
    let total = 0;

    if (this.items && this.items.length) {
      total += this.items.reduce((a, b) => a + b.amount, 0);
    }

    if (this.caterings && this.caterings.length) {
      total += this.caterings.reduce((a, b) => a + b.amount, 0);
    }

    if (this.customCatering  && this.customCatering.items && this.customCatering.items.length) {
      total += 1;
    }

    return total;
  }
}
