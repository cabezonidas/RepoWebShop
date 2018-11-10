import { Component, AfterViewInit, Input, OnInit, OnDestroy } from '@angular/core';
import { AppService } from 'src/app/core/services/app/app.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-fb-share',
  templateUrl: './fb-share.component.html',
  styleUrls: ['./fb-share.component.scss']
})
export class FbShareComponent implements AfterViewInit, OnInit, OnDestroy {

  @Input() url: string;
  @Input() showShare = false;
  @Input() showLike = false;

  scriptLoadedSub: Subscription;
  constructor(private appService: AppService) {
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
  }

  ngOnInit() {
    this.scriptLoadedSub = this.appService.fbScriptUrl.subscribe(loaded => {
      if (!loaded) {
        const script = document.createElement('script');
        script.src = 'https://connect.facebook.net/es_ES/sdk.js';
        document.body.appendChild(script);

        window['fbAsyncInit'] = function () {
            window['FB'].init({
                appId: '744831982380043',
                autoLogAppEvents: true,
                xfbml: true,
                version: 'v3.0'
            });
        };
        this.appService.fbScriptSource.next(true);
      }
    });
  }

  ngOnDestroy = () => this.scriptLoadedSub.unsubscribe();

  ngAfterViewInit() {
    // render facebook button
    // tslint:disable-next-line:no-unused-expression
    window['FB'] && window['FB'].XFBML.parse();
  }
}
