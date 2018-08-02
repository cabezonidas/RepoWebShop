import { Component, ViewChild, OnInit, OnDestroy } from '@angular/core';
import { CartService } from './cart/services/cart.service';
import { AuthService } from './authentication/services/auth.service';
import { Subscription } from 'rxjs';
import { MatSidenav } from '@angular/material/sidenav';
import { IAppUser } from './authentication/interfaces/iapp-user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
  // animations: [
  //   trigger('items', [
  //     transition('* => *', [
  //       query(':self', style({ opacity: 0}), {optional: true}),

  //       query(':self', stagger('300ms', [
  //         animate('.6s ease-in', keyframes([
  //           style({opacity: 0, transform: 'translateY(-75%)', offset: 0}),
  //           style({opacity: .5, transform: 'translateY(35px)', offset: .3}),
  //           style({opacity: 1, transform: 'translateY(0)', offset: 1})
  //         ]))]), {optional: true})
  //     ])
  //   ])
  // ]
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
