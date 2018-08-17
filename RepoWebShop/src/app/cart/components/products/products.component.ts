import { Component, OnInit, Input } from '@angular/core';
import { ICartItem } from '../../interfaces/icart-item';
import { ICartCatering } from '../../interfaces/icart-catering';
import { ICatering } from '../../../catering/interfaces/ICatering';
import { ICartCatalogItem } from '../../interfaces/icart-catalog-item';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  @Input() items: ICartCatalogItem[];
  @Input() caterings: ICartCatering[];
  @Input() customCatering: ICatering;
  
  constructor() { }

  ngOnInit() {
  }

}
