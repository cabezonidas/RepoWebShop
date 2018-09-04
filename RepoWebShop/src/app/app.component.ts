import { Component, ViewChild, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { AuthService } from './authentication/services/auth.service';
import { Subscription } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { IAppUser } from './authentication/interfaces/iapp-user';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  @ViewChild('sidenav') sidenav: MatSidenav;
  heightPadding = 0;

  user: IAppUser;
  userSub = new Subscription();

  @ViewChild('content') public contentElement: ElementRef;

  constructor(private auth: AuthService, private router: Router) {}

  ngOnInit() {
    this.userSub = this.auth.loadUser().subscribe(user => this.user = user);
    this.adjustPadding();
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

  adjustPadding = () => this.heightPadding = window.innerWidth < 600 ? 56 : 64;
  close = () => this.sidenav.close();
}
