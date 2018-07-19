import { Component, OnInit, HostBinding } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import { Router } from '@angular/router';
import { moveIn, fallIn, moveInLeft } from '../router.animations';
import { Observable } from '../../../../../node_modules/rxjs';

@Component({
  selector: 'app-other',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss'],
  animations: [moveIn(), fallIn(), moveInLeft()]
})

export class MembersComponent implements OnInit {

  user: firebase.User;

  constructor(public afAuth: AngularFireAuth, private router: Router) {
    this.afAuth.user.subscribe(user$ => {
      this.user = user$;
    });
  }

  @HostBinding('@moveIn') role = '';

  logout() {
    this.afAuth.auth.signOut();
    this.router.navigateByUrl('/login');
  }


  ngOnInit() {
  }

}
