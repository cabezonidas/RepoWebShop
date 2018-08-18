import { Component, OnInit, Input, Output, EventEmitter, ViewChild, ElementRef, OnChanges } from '@angular/core';
import { AsyncValidatorFn, AbstractControl, ValidationErrors, FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Observable, timer } from 'rxjs';
import { switchMap, map, tap } from 'rxjs/operators';
import { InvoiceService } from '../../services';
import * as fromStore from '../../store';
import { fallIn, moveInLeft } from '../../../animations/router.animations';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss'],
  animations: [fallIn(), moveInLeft()]
})
export class InvoiceComponent implements OnInit, OnChanges {
  @Input() savedCuit: string;
  @Input() cuitLoaded: boolean;
  @Input() cuitLoading: boolean;
  @Output() addCuit = new EventEmitter<string>();
  @Output() clearCuit = new EventEmitter<void>();
  @Output() next = new EventEmitter<void>();
  cuitGroup: FormGroup;
  @ViewChild('cuit') public cuitNumber: ElementRef;

  constructor(private _formBuilder: FormBuilder, private invoice: InvoiceService) { }

  ngOnInit() {
    this.cuitGroup = new FormGroup({
      'cuit': new FormControl('', [Validators.required], [this.isCuitValid()])
    });
  }
  
  isCuitValid(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
      return timer(1000).pipe(
        tap(() => this.cuitLoading = true),
        switchMap(() => this.invoice.isCuitValid(control.value)),
        map(isCodeValid => {
          this.cuitLoading = false;
          if (isCodeValid) {
            this.saveCuit(control.value as string);
            return null;
          } else {
            return { 'cuitInvalid': true }
          } 
        })
    )};
  };

  ngOnChanges() {
    console.log('saved cuit', this.savedCuit);
  }

  removeCuit = (): void => this.clearCuit.emit();

  continue = (): void => this.next.emit();

  saveCuit = (cuit: string): void => this.addCuit.emit(cuit);

}
