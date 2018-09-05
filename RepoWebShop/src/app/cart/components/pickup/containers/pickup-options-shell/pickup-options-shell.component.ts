import { Component, OnInit } from '@angular/core';
import { PickupService } from '../../../../services/pickup.service';
import { Observable } from 'rxjs';
import { IPickupOption } from '../../interfaces/ipickup-option';

@Component({
  selector: 'app-pickup-options-shell',
  templateUrl: './pickup-options-shell.component.html',
  styleUrls: ['./pickup-options-shell.component.scss']
})
export class PickupOptionsShellComponent implements OnInit {

  options$: Observable<IPickupOption[]>;
  constructor(public pickup: PickupService) { }

  ngOnInit() {
    this.options$ = this.pickup.loadPickupOptions();
  }

}
