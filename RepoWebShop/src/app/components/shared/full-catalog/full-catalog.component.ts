import { Component, OnInit, Input } from '@angular/core';
import { IItem } from '../../../interfaces/iitem';
import { IProduct } from '../../../interfaces/iproduct';
import { CartService } from '../../../services/cart.service';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-full-catalog',
  templateUrl: './full-catalog.component.html',
  styleUrls: ['./full-catalog.component.scss']
})
export class FullCatalogComponent implements OnInit {

  @Input() products$: Array<IProduct>;

  items;

  displayedColumns: string[] = [
    'displayName',
    'action'
  ];

  constructor(private cart: CartService) {}

  ngOnInit() {
    const items: Array<IItem> = [];
    this.products$.forEach(x => x.items.forEach(y => items.push(y)));
    this.items = new MatTableDataSource(items);
  }

  addToCart(id: MouseEvent): void {
    this.cart.addProductToCart(id);
  }

  applyFilter(filterValue: string) {
    this.items.filter = filterValue.trim().toLowerCase();
  }

}
