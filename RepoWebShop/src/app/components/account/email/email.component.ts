import { Component, OnInit, HostBinding } from '@angular/core';
// import { AngularFire, AuthProviders, AuthMethods } from 'angularfire2';
import { Router } from '@angular/router';
import { moveIn, fallIn } from '../../../animations/router.animations';
import { AuthService } from '../../../services/auth.service';
import { EmailLogin } from '../../../classes/EmailLogin';
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { HttpErrorResponse } from '../../../../../node_modules/@angular/common/http';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss'],
  animations: [moveIn(), fallIn()]
})
export class EmailComponent implements OnInit {

  state: '';
  rForm: FormGroup;
  error: any;
  email: '';
  password: '';
  emailAlert = 'Ingresa un email válido.';
  passAlert = 'Ingresa contraseña.';
  hide = true;
  returnUrl = '';

  constructor(private router: Router, private auth: AuthService, private fb: FormBuilder) {
    this.rForm = fb.group({
      'email': [null, Validators.compose([Validators.required, Validators.email])],
      'password': [null, Validators.required]
    });

  }

  @HostBinding('@moveIn') role = '';

  onSubmit(post: EmailLogin) {
    if (this.rForm.valid) {
      this.auth.emailLogin(new EmailLogin(post.email, post.password)).subscribe(this.auth.loadUser,
        (err: HttpErrorResponse) => {
          this.error = err.status === 401 ? 'Email o contraseña erróneo.' : 'Ocurrió un error y no se pudo iniciar sesión';
        });
    }
  }


  ngOnInit() {
    this.auth.returnUrl.subscribe(url => this.returnUrl = url);
    this.auth.user.subscribe(user$ => {
      if (user$) {
        this.router.navigate([ this.returnUrl ? this.returnUrl : '/members' ]);
      }
    });
  }
}
