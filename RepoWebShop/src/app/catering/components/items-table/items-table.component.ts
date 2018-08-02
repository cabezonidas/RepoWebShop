import { Component, OnInit, Input, OnChanges, ViewChild } from '@angular/core';
import { IItem } from '../../../products/interfaces/iitem';
import {MatTableDataSource, MatPaginator} from '@angular/material';


@Component({
  selector: 'app-items-table',
  templateUrl: './items-table.component.html',
  styleUrls: ['./items-table.component.scss']
})
export class ItemsTableComponent implements OnInit, OnChanges {
  @Input() items: IItem[];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'price', 'action'];
  dataSource = new MatTableDataSource(this.items);

  constructor() { }

  ngOnInit() {
    this.paginatorToSpanish();
  }

  ngOnChanges() {
    this.dataSource = new MatTableDataSource(this.items);

    this.dataSource.paginator = this.paginator;
  }


  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  paginatorToSpanish = () => {
    this.paginator._intl.firstPageLabel = 'Primera';
    this.paginator._intl.itemsPerPageLabel = 'Items por página';
    this.paginator._intl.lastPageLabel = 'Última';
    this.paginator._intl.nextPageLabel = 'Siguiente';
    this.paginator._intl.previousPageLabel = 'Anterior';
    this.paginator._intl.getRangeLabel = (page, pageSize, length) => {
      const start = !length ? 0 : (page * pageSize) + 1;
      const end = length - start > 5 ? start + pageSize - 1 : length;
      return start + ' - ' + end + ' de ' + length;
    };
  }
}
