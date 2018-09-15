import { Component, OnInit, Input, ViewChild, OnChanges } from '@angular/core';
import { IOrder } from '../../interfaces/iorder';
import { MatPaginator, MatSort, MatTableDataSource, Sort, MatDialog } from '@angular/material';
import { CalendarService } from '../../../home/services/calendar.service';
import { OrderDialogShellComponent } from '../order-dialog-shell/order-dialog-shell.component';
import { OrdersService } from '../../services/orders.service';

@Component({
  selector: 'app-orders-shell',
  templateUrl: './orders-shell.component.html',
  styleUrls: ['./orders-shell.component.scss']
})
export class OrdersShellComponent implements OnInit, OnChanges {

  displayedColumns: string[] = ['pickUpTimeFrom', 'items'];
  dataSource: MatTableDataSource<IOrder>;

  @Input() orders: IOrder[];
  sortedData: IOrder[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public calendar: CalendarService, public dialog: MatDialog, public orderService: OrdersService) {
  }

  openDialog(row: IOrder) {
    this.dialog.open(OrderDialogShellComponent, {
      data: row
    });
  }
  ngOnInit() {

  }

  ngOnChanges() {
    if (this.orders) {
      this.sortedData = this.orders.slice();
      this.dataSource = new MatTableDataSource(this.orders);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }

  sortData(sort: Sort) {
    const data = this.orders.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }

    this.sortedData = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'friendlyBookingId': return compare(a.friendlyBookingId, b.friendlyBookingId, isAsc);
        case 'orderTotal': return compare(a.orderTotal, b.orderTotal, isAsc);
        case 'pickUpTimeFrom': return compare(a.pickUpTimeFrom, b.pickUpTimeFrom, isAsc);
        default: return 0;
      }
    });
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
  archive(row: IOrder) {
    this.openDialog(row);
  }
  notify(row: IOrder) {
    this.openDialog(row);
  }
}

function compare(a, b, isAsc) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
