import { Component, OnInit } from '@angular/core';
import { IItem } from '../../../products/interfaces/iitem';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { map } from '../../../../../node_modules/rxjs/operators';

@Component({
  selector: 'app-new-catering-shell',
  templateUrl: './new-catering-shell.component.html',
  styleUrls: ['./new-catering-shell.component.scss']
})
export class NewCateringShellComponent implements OnInit {
  items$: Observable<IItem[]>;
  errorMessage$: Observable<string>;

  constructor(private store: Store<fromCatering.State>) { }

  ngOnInit() {
    this.store.dispatch(new cateringActions.LoadItems());
    this.items$ = this.store.pipe(
      select(fromCatering.getItems),
      map(items => items.sort((a, b) => a.displayName.localeCompare(b.displayName))
    ));
    this.errorMessage$ = this.store.pipe(select(fromCatering.getError));
  }

}
