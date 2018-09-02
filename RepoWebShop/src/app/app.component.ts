import { Component, ViewChild, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { AuthService } from './authentication/services/auth.service';
import { Subscription } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { IAppUser } from './authentication/interfaces/iapp-user';
import { ScrollService } from './home/services/scroll.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  @ViewChild('sidenav') sidenav: MatSidenav;

  contentHeight = 0;
  heightPadding = 0;

  user: IAppUser;
  userSub = new Subscription();
  reason = '';

  @ViewChild('content') public contentElement: ElementRef;

  constructor(private auth: AuthService, private scroll: ScrollService) {}
  ngOnInit() {
    this.userSub = this.auth.loadUser().subscribe(user => this.user = user);
    this.adjustPadding();
  }

  onResize = () => this.adjustHeight();

  adjustPadding() {
    this.heightPadding = window.innerWidth < 600 ? 56 : 64;
  }
  adjustHeight() {
    this.adjustPadding();
    if (this.contentElement.nativeElement.offsetHeight > window.innerHeight) {
      this.contentHeight = this.contentElement.nativeElement.offsetHeight;
    } else {
      this.contentHeight = window.innerHeight - this.heightPadding;
    }
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

  close(reason: string) {
    this.reason = reason;
    this.sidenav.close();
  }
}
