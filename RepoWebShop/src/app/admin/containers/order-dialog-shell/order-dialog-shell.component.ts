import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';
import { IOrder } from '../../interfaces/iorder';

@Component({
  selector: 'app-order-dialog-shell',
  templateUrl: './order-dialog-shell.component.html',
  styleUrls: ['./order-dialog-shell.component.scss']
})
export class OrderDialogShellComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) public data: IOrder) {}

  ngOnInit() {
  }

}
