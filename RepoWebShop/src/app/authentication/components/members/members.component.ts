import { Component, OnInit, HostBinding, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { moveIn, fallIn, moveInLeft } from '../../../animations/router.animations';
import { AuthService } from '../../services/auth.service';
import { IAppUser } from '../../interfaces/iapp-user';
import { AngularFireAuth } from 'angularfire2/auth';
import { Subscription } from 'rxjs';
import { AppService } from '../../../core/services/app/app.service';
import { Title } from '@angular/platform-browser';

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

  constructor(private router: Router, private auth: AuthService, private titleService: Title,
    private afAuth: AngularFireAuth, private appService: AppService) { }

  @HostBinding('@moveIn') role = '';

  logout() {
    this.afAuth.auth.signOut().then(() => {
      this.logOut = this.auth.logOut().subscribe(() => {
        this.appService.setUser(null);
      });
      // api logout doesn't work -> force log out with MVC engine
      window.location.assign('/account/LogoutSpa/start');
    });
  }

  ngOnInit() {
    this.titleService.setTitle('Perfil');
    this.userSub = this.appService.user.subscribe(user$ => {
      this.user = user$;
    });
    // this.userSub = this.auth.loadUser().subscribe(user => {
    //   this.user = user;
    // });
  }
  ngOnDestroy() {
    this.userSub.unsubscribe();
    this.logOut.unsubscribe();
  }
}
