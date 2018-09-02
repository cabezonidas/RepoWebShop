import { Component, OnInit, Input } from '@angular/core';
import { ICatering } from '../../interfaces/ICatering';

@Component({
  selector: 'app-catering-option',
  templateUrl: './catering-option.component.html',
  styleUrls: ['./catering-option.component.scss']
})
export class CateringOptionComponent implements OnInit {

  @Input() catering: ICatering;
  constructor() { }

  ngOnInit() {
    console.log(this.catering);
  }

}
