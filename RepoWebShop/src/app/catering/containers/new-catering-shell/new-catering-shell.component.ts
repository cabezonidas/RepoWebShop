import { Component, OnInit, HostBinding } from '@angular/core';
import { IItem } from '../../../products/interfaces/iitem';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { map } from 'rxjs/operators';
import { ICatering } from '../../interfaces/ICatering';
import { moveIn } from '../../../animations/router.animations';

@Component({
  selector: 'app-new-catering-shell',
  templateUrl: './new-catering-shell.component.html',
  styleUrls: ['./new-catering-shell.component.scss'],
  animations: [moveIn()]
})
export class NewCateringShellComponent implements OnInit {
  items$: Observable<IItem[]>;
  catering$: Observable<ICatering>;
  errorMessage$: Observable<string>;

  constructor(private store: Store<fromCatering.State>) { }
  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.store.dispatch(new cateringActions.LoadSessionCatering());
    this.catering$ = this.store.pipe(
      select(fromCatering.getSessionCatering)
    );

    this.store.dispatch(new cateringActions.LoadItems());
    this.items$ = this.store.pipe(
      select(fromCatering.getItems),
      map(items => items.sort((a, b) => a.displayName.localeCompare(b.displayName))
    ));

    this.errorMessage$ = this.store.pipe(select(fromCatering.getError));
  }

  addItem(itemId: number): void {
    this.store.dispatch(new cateringActions.AddItem(itemId));
  }

}

