import { Component, ViewChild, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from './authentication/services/auth.service';
import { Subscription } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { IAppUser } from './authentication/interfaces/iapp-user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  @ViewChild('sidenav') sidenav: MatSidenav;

  user: IAppUser;
  userSub = new Subscription();
  reason = '';

  constructor(private auth: AuthService) {}

  ngOnInit() {
    this.userSub = this.auth.loadUser().subscribe(user => this.user = user);
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

  close(reason: string) {
    this.reason = reason;
    this.sidenav.close();
  }
}
