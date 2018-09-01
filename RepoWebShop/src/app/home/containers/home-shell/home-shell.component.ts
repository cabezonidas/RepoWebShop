import { Component, OnInit } from '@angular/core';
import { ScrollService } from '../../services/scroll.service';

@Component({
  selector: 'app-home-shell',
  templateUrl: './home-shell.component.html',
  styleUrls: ['./home-shell.component.scss']
})
export class HomeShellComponent implements OnInit {

  constructor(private scroll: ScrollService) { }

  ngOnInit() {
  }
  triggerScrollTo(target: string, offset: number) {
    this.scroll.triggerScrollTo(target, offset);
  }
}
