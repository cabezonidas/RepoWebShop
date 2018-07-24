import { Component, OnInit, HostBinding, ViewChild } from '@angular/core';
import { moveIn, fallIn, moveInLeft } from '../../../animations/router.animations';
import { Router } from '../../../../../node_modules/@angular/router';
import { AuthService } from '../../../services/auth.service';
import { IAppUser } from '../../../interfaces/iapp-user';
import { FormGroup, FormBuilder, Validators } from '../../../../../node_modules/@angular/forms';

@Component({
  selector: 'app-mobile',
  templateUrl: './mobile.component.html',
  styleUrls: ['./mobile.component.scss'],
  animations: [moveIn(), fallIn(), moveInLeft()]
})
export class MobileComponent implements OnInit {

  user: IAppUser;
  state = '';
  mobileForm: FormGroup;

  constructor(private router: Router, private auth: AuthService, private fb: FormBuilder) {
    this.mobileForm = fb.group({
      'mobile': [null, Validators.required],
      'country': ['54', Validators.required]
    });
  }
  @HostBinding('@moveIn') role = '';

  ngOnInit() {
    this.auth.user.subscribe(user$ => {
      this.user = user$;
    });
  }


}
