import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, AbstractControl, FormGroup, Validators } from '@angular/forms';
import { DiscountService } from '../../services';
import { Observable, interval, of, fromEvent } from 'rxjs';
import { switchMap, map, flatMap, debounceTime, tap } from 'rxjs/operators';

@Component({
  selector: 'app-discount',
  templateUrl: './discount.component.html',
  styleUrls: ['./discount.component.scss']
})
export class DiscountComponent implements OnInit {

  isCodeValid = true;
  discountGroup: FormGroup;
  @ViewChild('discountCode') public discountCode: ElementRef;

  constructor(private _formBuilder: FormBuilder, private discount: DiscountService) { }

  ngOnInit() {
    this.discountGroup = this._formBuilder.group({
      discount: ['', Validators.required , this.isValid.bind(this)]
    });
  }
  
  // isValid(control: AbstractControl): {[key: string]: any} | null {
  //   return fromEvent(this.discountCode.nativeElement, 'keyup').pipe(
  //     map((e:any) => e.target.value as string),
  //     debounceTime(1000),
  //     switchMap((value) => this.discount.isValid(value)),
  //     map(isCodeValid => {
  //       this.isCodeValid = isCodeValid;
  //       if (!isCodeValid) {
  //         return !isCodeValid ? null : { isCodeInvalid: true };
  //       }
  //     })
  //   );
  // }
  
  isValid(control: AbstractControl): {[key: string]: any} | null {
    console.log('value', control.value);
    return this.discount.isValid(control.value).pipe(
      map(isCodeValid => {
        if (!isCodeValid) {
          this.isCodeValid = isCodeValid;
          return !isCodeValid ? null : { isCodeInvalid: true };
        }
      })
    );
  }
}