import { Component, AfterViewInit, Input } from '@angular/core';

@Component({
  selector: 'app-fb-share',
  templateUrl: './fb-share.component.html',
  styleUrls: ['./fb-share.component.scss']
})
export class FbShareComponent implements AfterViewInit {

  @Input() url: string;
  @Input() showShare = false;
  @Input() showLike = false;
  constructor() {
    // initialise facebook sdk after it loads if required
    if (!window['fbAsyncInit']) {
      window['fbAsyncInit'] = function () {
          window['FB'].init({
              appId: '744831982380043',
              autoLogAppEvents: true,
              xfbml: true,
              version: 'v3.0'
          });
      };
    }

    // load facebook sdk if required
    const url = 'https://connect.facebook.net/es_ES/sdk.js';
    if (!document.querySelector(`script[src='${url}']`)) {
        const script = document.createElement('script');
        script.src = url;
        document.body.appendChild(script);
    }
  }

  ngAfterViewInit() {
    // render facebook button
    // tslint:disable-next-line:no-unused-expression
    window['FB'] && window['FB'].XFBML.parse();
  }
}
