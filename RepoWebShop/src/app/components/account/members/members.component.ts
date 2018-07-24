import { Component, OnInit, HostBinding } from '@angular/core';
import { Router } from '@angular/router';
import { moveIn, fallIn, moveInLeft } from '../../../animations/router.animations';
import { AuthService } from '../../../services/auth.service';
import { IAppUser } from '../../../interfaces/iapp-user';

@Component({
  selector: 'app-other',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.scss'],
  animations: [moveIn(), fallIn(), moveInLeft()]
})

export class MembersComponent implements OnInit {

  user: IAppUser;
  state = '';

  constructor(private router: Router, private auth: AuthService) { }

  @HostBinding('@moveIn') role = '';

  logout() {
    this.auth.logOut();
  }

  ngOnInit() {
    this.auth.user.subscribe(user$ => {
      this.user = user$;
    });
  }

}
