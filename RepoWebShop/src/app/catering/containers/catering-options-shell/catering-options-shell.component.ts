import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';

import * as fromEffects from '../../../cart/store/effects';
import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { ICatering } from '../../interfaces/ICatering';
import { moveIn } from '../../../animations/router.animations';
import * as fromStore from '../../../cart/store';
import { filter } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-catering-options-shell',
  templateUrl: './catering-options-shell.component.html',
  styleUrls: ['./catering-options-shell.component.scss'],
  animations: [moveIn()]
})
export class CateringOptionsShellComponent implements OnInit, OnDestroy {
  cateringCopiedSub = new Subscription();
  caterings$: Observable<ICatering[]>;
  carouselInit = false;
  slideConfig = {
    arrows: false,
    slidesToShow: 1,
    slidesToScroll: 1,
    variableWidth: true,
    centerMode: true,
    centerPadding: '40px',
    infinite: true,
  };

  constructor(
    private store: Store<fromCatering.State>,
    private itemEffects: fromEffects.CateringsEffects,
    private router: Router
  ) { }
  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.store.dispatch(new cateringActions.LoadCaterings());
    this.caterings$ = this.store.pipe(select(fromCatering.getCaterings));
    this.cateringCopiedSub = this.itemEffects.copyCatering$
      .pipe(filter(action => action.type === fromStore.CateringActionTypes.CopyCateringSuccess))
      .subscribe(() => this.router.navigateByUrl('/new-catering'));
  }
  ngOnDestroy() {
    this.cateringCopiedSub.unsubscribe();
  }

  addCatering = (id: number) => this.store.dispatch(new fromStore.AddCatering(id));
  copyCatering = (id: number) => {
    this.store.dispatch(new fromStore.CopyCatering(id));
  }
}
