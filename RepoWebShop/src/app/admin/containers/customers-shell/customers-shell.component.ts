import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ICustomer } from '../../interfaces/icustomer';
import { CustomerService } from '../../services/customer.service';

@Component({
  selector: 'app-customers-shell',
  templateUrl: './customers-shell.component.html',
  styleUrls: ['./customers-shell.component.scss']
})
export class CustomersShellComponent implements OnInit {

  customers$: Observable<ICustomer[]>;
  constructor(public customers: CustomerService) { }

  ngOnInit() {
    this.customers$ = this.customers.getAll();
  }

}
