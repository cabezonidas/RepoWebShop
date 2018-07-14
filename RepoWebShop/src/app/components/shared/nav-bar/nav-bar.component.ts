import { Component, OnChanges, Input } from '@angular/core';
import { ICartCatalogItem } from '../../../interfaces/icart-catalog-item';
import { trigger, style, transition, animate, keyframes, query, stagger } from '@angular/animations';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
  animations: [
    trigger('items', [
      transition('* => *', [
        query(':self', style({ opacity: 0}), {optional: true}),

        query(':self', stagger('300ms', [
          animate('.6s ease-in', keyframes([
            style({opacity: 0, transform: 'translateY(-75%)', offset: 0}),
            style({opacity: .5, transform: 'translateY(35px)', offset: .3}),
            style({opacity: 1, transform: 'translateY(0)', offset: 1})
          ]))]), {optional: true})
      ])
    ])
  ]
})
export class NavBarComponent implements OnChanges {

  @Input() products$: Array<ICartCatalogItem>;

  ngOnChanges() {
    console.log(this.products$);
  }
}
