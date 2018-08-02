import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { moveIn, fallIn, moveInLeft } from '../../../animations/router.animations';
import { AuthService } from '../../services/auth.service';
import { IAppUser } from '../../interfaces/iapp-user';
import { AngularFireAuth } from 'angularfire2/auth';
import { Subscription } from 'rxjs';
import { AppService } from '../../../core/services/app/app.service';

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

  constructor(private router: Router, private auth: AuthService, private afAuth: AngularFireAuth, private appService: AppService) { }

  @HostBinding('@moveIn') role = '';

  logout() {
    this.afAuth.auth.signOut().then(() => {
      this.logOut = this.auth.logOut().subscribe(() => this.appService.setUser(null));
    });
  }

  ngOnInit() {
    this.userSub = this.auth.loadUser().subscribe(user => {
      this.user = user;
    });
  }
  ngOnDestroy() {
    this.userSub.unsubscribe();
    this.logOut.unsubscribe();
  }
}
