import { Component, ViewChild, OnInit, OnDestroy, ElementRef } from '@angular/core';
import { AuthService } from './authentication/services/auth.service';
import { Subscription } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { IAppUser } from './authentication/interfaces/iapp-user';
import { Store } from '@ngrx/store';
import { AppService } from './core/services/app/app.service';
import * as fromProduct from './products/state';
import * as productActions from './products/state/product.actions';
import * as cateringActions from './catering/state/catering.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [AppService]
})
export class AppComponent implements OnInit, OnDestroy {
  @ViewChild('sidenav') sidenav: MatSidenav;
  heightPadding = 0;

  user: IAppUser;
  userSub = new Subscription();

  @ViewChild('content') public contentElement: ElementRef;

  constructor(private store: Store<fromProduct.State>, private auth: AuthService, private app: AppService) {}

  ngOnInit() {
    this.userSub = this.auth.loadUser().subscribe(user => {
      this.app.setUser(user);
      this.user = user;
    });
    this.adjustPadding();
    this.store.dispatch(new productActions.LoadProducts());
    this.store.dispatch(new cateringActions.LoadCaterings());
  }

  ngOnDestroy(): void {
    this.userSub.unsubscribe();
  }

  adjustPadding = () => this.heightPadding = window.innerWidth < 600 ? 56 : 64;
  close = () => this.sidenav.close();
}
