import { Component, OnInit, Input, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { CustomerService } from '../../services/customer.service';
import { Subscription, fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map, switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-email-activation-code',
  templateUrl: './email-activation-code.component.html',
  styleUrls: ['./email-activation-code.component.scss']
})
export class EmailActivationCodeComponent implements OnInit, OnDestroy {

  validationCode: string;
  sub = new Subscription();
  keyword: boolean;
  @ViewChild('email') input: ElementRef;

  constructor(private customer: CustomerService) { }

  ngOnInit() {
    fromEvent(this.input.nativeElement, 'keyup').pipe(
      map((e: any) => e.target.value as string),
      debounceTime(1500),
      distinctUntilChanged(),
      tap(_ => this.keyword = false),
      filter(email => this.validEmail(email)),
      tap(_ => this.keyword = true),
      switchMap(email => this.customer.getEmailActivationCode(email))
    ).subscribe(code => {
      if (code) {
        this.keyword = true;
        this.validationCode = code;
      } else {
        this.validationCode = null;
      }
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  validEmail(email) {
    // tslint:disable-next-line:max-line-length
    const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
  }

}
