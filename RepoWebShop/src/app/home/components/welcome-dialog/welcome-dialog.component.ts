import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { CalendarService } from '../../services/calendar.service';
import { Subscription } from 'rxjs';
import { IPublicCalendar } from '../../interfaces/ipublic-calendar';
import { map, tap, groupBy, switchMap, toArray, mergeMap } from 'rxjs/operators';
import { IWorkingHour } from '../../interfaces/iworking-hour';
import { IPublicHoliday } from '../../interfaces/ipublic-holiday';
import { IVacation } from '../../interfaces/ivacation';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-welcome-dialog',
  templateUrl: './welcome-dialog.component.html',
  styleUrls: ['./welcome-dialog.component.scss']
})
export class WelcomeDialogComponent implements OnInit, OnDestroy {

  constructor(
    private dialogRef: MatDialogRef<WelcomeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
  }

  ngOnInit() {
    
  }

  ngOnDestroy() {
  }

  close() {
    this.dialogRef.close();
  }
}
