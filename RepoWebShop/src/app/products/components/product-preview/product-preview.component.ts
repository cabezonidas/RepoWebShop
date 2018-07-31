import { Component, Input, HostBinding } from '@angular/core';
import { MatBottomSheet } from '@angular/material';
import { ChooseItemComponent } from '../choose-item/choose-item.component';
import { IProduct } from '../../interfaces/iproduct';
import { IItem } from '../../interfaces/iitem';

@Component({
  selector: 'app-product-preview',
  templateUrl: './product-preview.component.html',
  styleUrls: ['./product-preview.component.scss']
})
export class ProductPreviewComponent {

  @Input() product: IProduct;

  constructor(private bottomSheet: MatBottomSheet) {}

  chooseProduct(): void {
    this.bottomSheet.open(ChooseItemComponent, {
      data: this.product.items as Array<IItem>
    });
  }
}
