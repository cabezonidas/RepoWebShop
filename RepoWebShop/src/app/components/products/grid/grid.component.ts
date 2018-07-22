import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../../services/products.service';
import { IProduct } from '../../../interfaces/iproduct';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-products',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {

  products$: Array<IProduct>;
  productsPreview$: Array<IProduct>;

  constructor(private products: ProductsService, private auth: AuthService) { }

  ngOnInit() {
    this.products.getProducts().subscribe(
      products => {
        this.products$ = products;
        this.productsPreview$ = products.filter(x => x.flickrAlbumId);
      }
    );
  }
}


