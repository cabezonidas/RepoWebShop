import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { IPickupOption } from '../../interfaces/ipickup-option';

@Component({
  selector: 'app-pickup-options',
  templateUrl: './pickup-options.component.html',
  styleUrls: ['./pickup-options.component.scss']
})
export class PickupOptionsComponent implements OnInit, OnChanges {

  @Input() options: IPickupOption[];

  constructor() { }

  ngOnInit() {

  }

  ngOnChanges() {
    // console.log(this.options);
  }

}
