import { Component, OnInit, Input, ViewChild, OnChanges } from '@angular/core';
import { ICustomer } from '../../interfaces/icustomer';
import { MatPaginator, MatSort, MatTableDataSource, Sort } from '@angular/material';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent implements OnInit, OnChanges {

  @Input() customers: Array<ICustomer>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  dataSource: MatTableDataSource<ICustomer>;
  sortedData: Array<ICustomer>;
  displayedColumns: string[] = ['name', 'spent', 'created'];

  constructor() { }

  ngOnInit() {
    this.feedTable();
  }

  ngOnChanges() {
    this.feedTable();
  }

  feedTable() {
    if (this.customers && this.customers.length) {
      this.sortedData = this.customers.slice();
      this.dataSource = new MatTableDataSource(this.customers);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  sortData(sort: Sort) {
    const data = this.customers.slice();
    if (!sort.active || sort.direction === '') {
      this.sortedData = data;
      return;
    }

    this.sortedData = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'spent': return compare(a.spent, b.spent, isAsc);
        case 'created': return compare(a.created, b.created, isAsc);
        default: return 0;
      }
    });
  }
  suffix(text: string, length: number): string {
    if (text.length <= length) {
      return text;
    } else {
      return text.substr(text.length - length, length);
    }
  }
}

function compare(a, b, isAsc) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
