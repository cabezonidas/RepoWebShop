import { Component, OnInit, Input } from '@angular/core';
import { ICustomer } from '../../interfaces/icustomer';

@Component({
  selector: 'app-customers-state',
  templateUrl: './customers-state.component.html',
  styleUrls: ['./customers-state.component.scss']
})
export class CustomersStateComponent implements OnInit {

  @Input() customer: ICustomer;
  recentlyActivatedFlag = false;
  constructor() { }

  ngOnInit() {
  }

  recentlyActivated = () => this.recentlyActivatedFlag = true;
}
