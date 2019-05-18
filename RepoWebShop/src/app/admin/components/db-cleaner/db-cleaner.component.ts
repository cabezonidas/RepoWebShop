import { Component, OnInit, OnDestroy, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { CleanupService } from '../../services/cleanup.service';
import { Subscribable, Subscription, fromEvent, BehaviorSubject } from 'rxjs';
import { tap, switchMap, mergeMap, map, filter } from 'rxjs/operators';

@Component({
  selector: 'app-db-cleaner',
  templateUrl: './db-cleaner.component.html',
  styleUrls: ['./db-cleaner.component.scss']
})
export class DbCleanerComponent implements OnInit, OnDestroy, AfterViewInit {

  cleanUpSubscription = new Subscription();
  cleanUpSubscriptionCycle = new Subscription();
  moreToClean = new BehaviorSubject<number>(0);
  loading = false;
  @ViewChild('cleanUp') cleanupButton: ElementRef;

  constructor(private cleanUp: CleanupService) { }

  ngOnInit() {
  }
  ngAfterViewInit() {
    this.cleanUpSubscription = fromEvent(this.cleanupButton.nativeElement, 'click').pipe(
        filter(_ => !this.loading),
        tap(() => {
          console.log('click running');
          this.loading = true;
        }),
        switchMap(() => this.cleanUp.cleanSessionData(20)),
        map(result => {
          const { cartDataRows, cartDatesRows, cartLunchesRows, cartProductsRows, lunchesRows } = result;
          const total = cartDataRows + cartDatesRows + cartLunchesRows + cartProductsRows + lunchesRows;
          console.log('click running, cleared: ' + total);
          return total;
        })
    ).subscribe(total => {
      if (total > 0) {
        this.moreToClean.next(total);
      } else {
        this.loading = false;
      }
    }, () => this.loading = false);

    this.cleanUpSubscriptionCycle = this.moreToClean.pipe(
      filter(total => total > 0),
      switchMap(() => this.cleanUp.cleanSessionData(20)),
      map(result => {
        const { cartDataRows, cartDatesRows, cartLunchesRows, cartProductsRows, lunchesRows } = result;
        const total = cartDataRows + cartDatesRows + cartLunchesRows + cartProductsRows + lunchesRows;
        console.log('cycle running, cleared: ' + total);
        return total;
      })
    ).subscribe(total => {
      this.moreToClean.next(total);
      if (total === 0) {
        this.loading = false;
      }
    }, () => this.loading = false);
  }

  ngOnDestroy() {
    this.cleanUpSubscription.unsubscribe();
    this.cleanUpSubscriptionCycle.unsubscribe();
  }
}
