import { Component, OnInit, OnChanges, Input } from '@angular/core';
import { trigger, transition, query, animate, style, stagger, keyframes } from '@angular/animations';
import { ICatering } from '../../interfaces/ICatering';

@Component({
  selector: 'app-new-catering-subtotal-header',
  templateUrl: './new-catering-subtotal-header.component.html',
  styleUrls: ['./new-catering-subtotal-header.component.scss'],
  animations: [
    trigger('subtotal', [
      transition('* => *', [
        animate('.6s ease-in', keyframes([
          style({opacity: 0, transform: 'translateY(-75%)', offset: 0}),
          style({opacity: .5, transform: 'translateY(35px)', offset: .3}),
          style({opacity: 1, transform: 'translateY(0)', offset: 1})
        ]))
      ])
  ])]
})
export class NewCateringSubtotalHeaderComponent implements OnInit, OnChanges {
  @Input() catering: ICatering;
  subtotal = 0;
  subtotalInStore = 0;
  constructor() { }

  ngOnInit() {
  }

  ngOnChanges() {
    if (this.catering && this.catering.items && this.catering.miscellanea) {
      this.subtotal = 0;
      this.subtotalInStore = 0;
      this.catering.items.forEach(i => this.subtotal += i.subTotal);
      this.catering.miscellanea.forEach(i => this.subtotal += i.price);
      this.catering.items.forEach(i => this.subtotalInStore += i.subTotalInStore);
      this.catering.miscellanea.forEach(i => this.subtotalInStore += i.price);
    }
  }

}
