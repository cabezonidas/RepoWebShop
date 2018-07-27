import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { moveIn, fallIn, moveInLeft } from '../../../animations/router.animations';
import { AuthService } from '../../../services/auth.service';
import { IAppUser } from '../../../interfaces/iapp-user';
import { AngularFireAuth } from '../../../../../node_modules/angularfire2/auth';
import { Subscription } from '../../../../../node_modules/rxjs';

@Component({
  selector: 'app-other',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss'],
  animations: [moveIn(), fallIn(), moveInLeft()]
})

export class MembersComponent implements OnInit, OnDestroy {

  user: IAppUser;
  state = '';
  logOut = new Subscription();
  userSub = new Subscription();

  constructor(private router: Router, private auth: AuthService, private afAuth: AngularFireAuth) { }

  @HostBinding('@moveIn') role = '';

  logout() {
    this.afAuth.auth.signOut().then(() => {
      this.logOut = this.auth.logOut().subscribe(() => this.auth.userSource.next(null));
    });
  }

  ngOnInit() {
    this.userSub = this.auth.user.subscribe(user$ => {
      this.user = user$;
      if (!user$) {
        this.router.navigateByUrl('/login');
      }
    });
  }
  ngOnDestroy() {
    this.userSub.unsubscribe();
    this.logOut.unsubscribe();
  }
}
