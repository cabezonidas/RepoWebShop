import { Injectable } from '@angular/core';
import { ScrollToService, ScrollToConfigOptions } from '@nicky-lenaers/ngx-scroll-to';

@Injectable({
  providedIn: 'root'
})
export class ScrollService {

  constructor(private _scrollToService: ScrollToService) { }

  public triggerScrollTo(target: string, offset: number = 65) {

    const config: ScrollToConfigOptions = {
      offset,
      target: target
    };
    this._scrollToService.scrollTo(config);
  }
  public instantScrollTo(target: string, offset: number = 65) {

    const config: ScrollToConfigOptions = {
      offset,
      target: target,
      duration: 1
    };
    this._scrollToService.scrollTo(config);
  }
}
