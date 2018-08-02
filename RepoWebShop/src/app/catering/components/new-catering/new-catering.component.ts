import { Component, OnInit, HostBinding, Input } from '@angular/core';
import { moveIn } from '../../../animations/router.animations';
import { IItem } from '../../../products/interfaces/iitem';

@Component({
  selector: 'app-new-catering',
  templateUrl: './new-catering.component.html',
  styleUrls: ['./new-catering.component.scss'],
  animations: [moveIn()]
})
export class NewCateringComponent implements OnInit {

  @Input() items: IItem[];
  constructor() { }

  @HostBinding('@moveIn') role = '';

  ngOnInit() {
  }

}
