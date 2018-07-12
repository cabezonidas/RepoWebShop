import { Component, OnInit, Input } from '@angular/core';
import { MatBottomSheet, MatBottomSheetRef } from '@angular/material';
import { IProduct } from '../../../interfaces/iproduct';
import { IItem } from '../../../interfaces/iitem';
import { ChooseItemComponent } from '../../products/choose-item/choose-item.component';

@Component({
  selector: 'app-product-preview',
  templateUrl: './product-preview.component.html',
  styleUrls: ['./product-preview.component.scss']

})
export class ProductPreviewComponent implements OnInit {

  @Input() product: IProduct;
  constructor(private bottomSheet: MatBottomSheet) {}

  ngOnInit() {
  }
  chooseProduct(): void {
    this.bottomSheet.open(ChooseItemComponent, {
      data: this.product.items as Array<IItem>
    });
  }
}
