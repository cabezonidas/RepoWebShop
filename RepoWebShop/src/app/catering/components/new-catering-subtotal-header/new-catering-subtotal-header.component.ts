import { Component, OnInit, OnChanges, Input } from '@angular/core';
import { trigger, transition, query, animate, style, stagger, keyframes } from '@angular/animations';
import { ICatering } from '../../interfaces/ICatering';
import { CateringService } from '../../services/catering.service';

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
  constructor(private cateringService: CateringService) { }

  ngOnInit() {
  }

  ngOnChanges() {
      this.subtotal = this.cateringService.totalOnline(this.catering);
      this.subtotalInStore = this.cateringService.totalInStore(this.catering);
  }
}
