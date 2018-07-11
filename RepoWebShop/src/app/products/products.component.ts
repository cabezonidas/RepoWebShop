import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../products.service';
import { IProduct } from '../interfaces/iproduct';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  products$: Array<IProduct>;

  constructor(private products: ProductsService) { }

  ngOnInit() {
    this.products.getProducts().subscribe(
      products => this.products$ = products
    );
  }
}


