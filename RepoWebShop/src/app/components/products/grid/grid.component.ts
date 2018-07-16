import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../../services/products.service';
import { IProduct } from '../../../interfaces/iproduct';

@Component({
  selector: 'app-products',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {

  products$: Array<IProduct>;

  constructor(private products: ProductsService) { }

  ngOnInit() {
    this.products.getProducts().subscribe(
      products => {
        this.products$ = products;
      }
    );
  }
}


