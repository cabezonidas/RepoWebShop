import { Component, OnInit, Input, OnDestroy, Output, ElementRef, ViewChild } from '@angular/core';
import { Subscription, fromEvent } from 'rxjs';
import { CustomerService } from '../../services/customer.service';
import { EventEmitter } from '@angular/core';
import { throttleTime, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-mobile-activation-code',
  templateUrl: './mobile-activation-code.component.html',
  styleUrls: ['./mobile-activation-code.component.scss']
})
export class MobileActivationCodeComponent implements OnInit, OnDestroy {

  @Input() userId: string;
  @Output() activated = new EventEmitter();
  validationCode: string;
  sub1 = new Subscription();
  sub2 = new Subscription();
  recentlyActivated = false;
  @ViewChild('validate') validate: ElementRef;
  constructor(private customer: CustomerService) { }

  ngOnInit() {
    this.sub1 = this.customer.getMobileActivationCode(this.userId).subscribe(code => this.validationCode = code);
    this.sub2 = fromEvent(this.validate.nativeElement, 'click').pipe(
      throttleTime(1500),
      switchMap(_ => this.customer.activateMobile(this.userId))
    ).subscribe(_ => {
      this.recentlyActivated = true;
      this.validationCode = null;
    });
  }

  ngOnDestroy() {
    this.sub1.unsubscribe();
    this.sub2.unsubscribe();
  }

}
