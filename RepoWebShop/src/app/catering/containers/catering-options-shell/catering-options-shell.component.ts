import { Component, OnInit, HostBinding, OnDestroy, ViewChild, ElementRef, AfterViewInit, OnChanges } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import * as M from 'materialize-css';

import * as fromEffects from '../../../cart/store/effects';
import * as fromCatering from '../../state';
import * as cateringActions from '../../state/catering.actions';
import { ICatering } from '../../interfaces/ICatering';
import { moveIn } from '../../../animations/router.animations';
import * as fromStore from '../../../cart/store';
import { filter } from 'rxjs/operators';
import { Router } from '@angular/router';
import { SlickComponent } from 'ngx-slick';
import { MatButton } from '@angular/material';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-catering-options-shell',
  templateUrl: './catering-options-shell.component.html',
  styleUrls: ['./catering-options-shell.component.scss'],
  animations: [moveIn()]
})
export class CateringOptionsShellComponent implements OnInit, AfterViewInit, OnDestroy {

  cateringCopiedSub = new Subscription();
  caterings$: Observable<ICatering[]>;
  loaded$: Observable<boolean>;
  carouselInit = false;
  slideConfig = {
    arrows: false,
    slidesToShow: 1,
    slidesToScroll: 1,
    variableWidth: true,
    // centerMode: true,
    // centerPadding: '40px',
    infinite: false, // This when set true, breaks clicks on slides
  };

  @ViewChild('slickModal') slickModal: SlickComponent;
  constructor(
    private store: Store<fromCatering.State>,
    private itemEffects: fromEffects.CateringsEffects,
    private router: Router,
    private titleService: Title
  ) { }
  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.titleService.setTitle('Catering para eventos');
    // this.store.dispatch(new cateringActions.LoadCaterings());
    this.caterings$ = this.store.pipe(select(fromCatering.getCaterings));
    this.loaded$ = this.store.pipe(select(fromCatering.getCateringsLoaded));
    this.cateringCopiedSub = this.itemEffects.copyCatering$
      .pipe(filter(action => action.type === fromStore.CateringActionTypes.CopyCateringSuccess))
      .subscribe(() => this.router.navigateByUrl('/new-catering'));
  }

  showArrows = () => {
    document.getElementById('slick-carousel');
  }

  ngAfterViewInit() {
    (document.getElementById('next') as HTMLElement).click();
  }

  ngOnDestroy() {
    this.cateringCopiedSub.unsubscribe();
  }

  slickPrev() {
    if (this.slickModal) {
      this.slickModal.slickPrev();
    }
  }

  slickNext() {
    try {
      this.slickModal.slickNext();
    } catch {}
  }

  addCatering = (id: number) => this.store.dispatch(new fromStore.AddCatering(id));
  copyCatering = (id: number) => {
    this.store.dispatch(new fromStore.CopyCatering(id));
  }
}
