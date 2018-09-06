import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppService } from '../../../core/services/app/app.service';
import { Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthService } from '../../../authentication/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-gear',
  templateUrl: './admin-gear.component.html',
  styleUrls: ['./admin-gear.component.scss']
})
export class AdminGearComponent implements OnInit, OnDestroy {

  isAdministrator = false;
  userSub = new Subscription();
  constructor(private appService: AppService, private auth: AuthService, private router: Router) { }

  ngOnInit() {
    this.userSub = this.appService.user.pipe(
      switchMap(() => this.auth.isAdmin())
    ).subscribe(isAdmin => this.isAdministrator = isAdmin);
  }

  ngOnDestroy() {
    this.userSub.unsubscribe();
  }

  goToAdmin = () => window.location.assign('/admin/index');
  goToHome = () => this.router.navigateByUrl('/start');
}
