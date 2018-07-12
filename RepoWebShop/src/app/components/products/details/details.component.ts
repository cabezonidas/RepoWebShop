import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ProductsService } from '../../../services/products.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  product$: Object;

  constructor(private products: ProductsService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => this.product$ = params.id);
  }

  ngOnInit() {
    this.products.getProduct(this.product$).subscribe(
      product => this.product$ = product
    );
  }
}
