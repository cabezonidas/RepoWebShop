import { Component, OnInit, ViewChild, ElementRef, OnChanges, Output, EventEmitter, AfterViewInit, OnDestroy, Input } from '@angular/core';
import { FormBuilder, AbstractControl, FormGroup, Validators,
  AsyncValidatorFn, ValidationErrors, FormControl, Validator } from '@angular/forms';
import { DiscountService } from '../../services';
import { Observable, interval, of, fromEvent, timer, Subscription } from 'rxjs';
import { switchMap, map, flatMap, debounceTime, tap, distinctUntilChanged } from 'rxjs/operators';
import { IDiscount } from '../../interfaces/idiscount';

@Component({
  selector: 'app-discount',
  templateUrl: './discount.component.html',
  styleUrls: ['./discount.component.scss']
})
export class DiscountComponent implements OnInit, AfterViewInit, OnDestroy, OnChanges {

  @Input() savedDiscount: IDiscount;
  @Input() discountsLoading: boolean;
  @Input() discountsLoaded: boolean;

  isCodeValid = true;
  discountStream$ = new Subscription();
  discountGroup: FormGroup;
  code = '';
  @ViewChild('discount') public discountCode: ElementRef;
  @Output() next = new EventEmitter<void>();
  @Output() applyDiscount = new EventEmitter<string>();
  @Output() clearDiscount = new EventEmitter<void>();

  constructor(private _formBuilder: FormBuilder, private discount: DiscountService) { }

  ngOnInit() {
    this.discountGroup = new FormGroup({
      'discount': new FormControl(this.code, [], [
        this.exists(), this.isActive(), this.isAvailable(), this.minOrderReached(),
        this.isValidToday(), this.notExpired(), this.notPending()
      ])
    });
  }

  ngOnChanges() {
    console.log('Changes', this.savedDiscount);
  }

  ngAfterViewInit() {
    this.discountStream$ = fromEvent(this.discountCode.nativeElement, 'keyup')
      .pipe(
        map((e: any) => e.target.value as string),
        debounceTime(500),
        distinctUntilChanged(),
        tap(code => { if (code) { this.applyDiscount.emit(code); }})
      )
      .subscribe();
  }

  ngOnDestroy() {
    this.discountStream$.unsubscribe();
  }

  onNext() {
    this.next.emit();
  }

  exists(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      if (!control.value) { return of(null); }
      return timer(1000).pipe(
        switchMap(() => this.discount.exists(control.value)),
        map(isCodeValid => isCodeValid ? null : { 'notExists': true })
    ); };
  }

  isActive(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      if (!control.value) { return of(null); }
      return timer(1000).pipe(
        switchMap(() => this.discount.isActive(control.value)),
        map(isCodeValid => isCodeValid ? null : { 'notActive': true })
    ); };
  }

  isAvailable(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      if (!control.value) { return of(null); }
      return timer(1000).pipe(
        switchMap(() => this.discount.isAvailable(control.value)),
        map(isCodeValid => isCodeValid ? null : { 'notAvailable': true })
    ); };
  }

  minOrderReached(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      if (!control.value) { return of(null); }
      return timer(1000).pipe(
        switchMap(() => this.discount.minOrderReached(control.value)),
        map(isCodeValid => isCodeValid ? null : { 'minOrderNotReached': true })
    ); };
  }

  isValidToday(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      if (!control.value) { return of(null); }
      return timer(1000).pipe(
        switchMap(() => this.discount.isValidToday(control.value)),
        map(isCodeValid => isCodeValid ? null : { 'notValidToday': true })
    ); };
  }

  notExpired(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      if (!control.value) { return of(null); }
      return timer(1000).pipe(
        switchMap(() => this.discount.notExpired(control.value)),
        map(isCodeValid => isCodeValid ? null : { 'expired': true })
    ); };
  }

  notPending(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      if (!control.value) { return of(null); }
      return timer(1000).pipe(
        switchMap(() => this.discount.notPending(control.value)),
        map(isCodeValid => isCodeValid ? null : { 'pending': true })
    ); };
  }
}
