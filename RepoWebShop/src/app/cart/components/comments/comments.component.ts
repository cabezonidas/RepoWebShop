import { Component, OnInit, Input, Output, EventEmitter, AfterViewInit, ElementRef, ViewChild, OnDestroy, OnChanges } from '@angular/core';
import { fromEvent, Subscription, of } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, tap, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit, AfterViewInit, OnDestroy, OnChanges {
  @Input() savedComments: string;
  @Input() commentsLoaded: boolean;
  @Input() commentsLoading: boolean;
  @Output() addComments = new EventEmitter<string>();
  @Output() next = new EventEmitter<void>();

  commentsStream$ = new Subscription();

  @ViewChild('orderComments') public orderComments: ElementRef;

  constructor() {
  }

  ngOnInit() {
  }

  ngOnChanges() {
  }

  ngAfterViewInit() {
    this.commentsStream$ = fromEvent(this.orderComments.nativeElement, 'keyup')
      .pipe(
        map((e: any) => e.target.value as string),
        debounceTime(500),
        distinctUntilChanged(),
        tap(comments => this.addComments.emit(comments))
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.commentsStream$.unsubscribe();
  }
}
