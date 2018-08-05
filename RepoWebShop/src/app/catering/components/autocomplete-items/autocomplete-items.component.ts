import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';
import { IItem } from '../../../products/interfaces/iitem';
import { MatAutocompleteSelectedEvent } from '@angular/material/typings';

@Component({
  selector: 'app-autocomplete-items',
  templateUrl: './autocomplete-items.component.html',
  styleUrls: ['./autocomplete-items.component.scss']
})
export class AutocompleteItemsComponent implements OnInit {

  @Input() items: IItem[];
  @Output() addItem = new EventEmitter<number>();

  itemCtrl = new FormControl();
  filteredProducts: Observable<IItem[]>;

  constructor ( ) {
    this.filteredProducts = this.itemCtrl.valueChanges
    .pipe(
      startWith(''),
      map(item => item ? this._filterItems(item) : this.items.slice())
    );
   }

  ngOnInit() {
  }

  _filterItems(value: string): IItem[]  {
    const searchTerm = value.trim().toLowerCase();

    return this.items.filter(item => {
      return item.displayName.toLowerCase().includes(searchTerm)
      || item.displayDescription.toLowerCase().includes(searchTerm)
      || item.category.toLowerCase().includes(searchTerm)
      || item.flavour.toLowerCase().includes(searchTerm);
    });
  }

  _onSelect(item: IItem) {
    this.addItem.emit(item.productId);
  }

}
