import { Component, OnInit, HostBinding } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { map } from 'rxjs/operators';
import { ICatering } from '../../interfaces/ICatering';
import { moveIn } from '../../../animations/router.animations';

@Component({
  selector: 'app-catering-options-shell',
  templateUrl: './catering-options-shell.component.html',
  styleUrls: ['./catering-options-shell.component.scss'],
  animations: [moveIn()]
})
export class CateringOptionsShellComponent implements OnInit {
  caterings$: Observable<ICatering[]>;

  constructor(private store: Store<fromCatering.State>) { }
  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.store.dispatch(new cateringActions.LoadCaterings());
    this.caterings$ = this.store.pipe(
      select(fromCatering.getCaterings)
    );
  }

}
