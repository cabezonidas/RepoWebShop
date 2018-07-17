import { Component, OnInit, Inject } from '@angular/core';
import { ICartCatalogItem } from '../../../interfaces/icart-catalog-item';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';

@Component({
  selector: 'app-cart-item-edit',
  templateUrl: './cart-item-edit.component.html',
  styleUrls: ['./cart-item-edit.component.scss']
})
export class CartItemEditComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<CartItemEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ICartCatalogItem) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

}
