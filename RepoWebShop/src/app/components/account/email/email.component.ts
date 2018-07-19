import { Component, OnInit, HostBinding } from '@angular/core';
// import { AngularFire, AuthProviders, AuthMethods } from 'angularfire2';
import { Router } from '@angular/router';
import { moveIn, fallIn } from '../router.animations';
import { trigger } from '@angular/animations';

@Component({
  selector: 'app-email',
  templateUrl: './email.component.html',
  styleUrls: ['./email.component.scss'],
  animations: [moveIn(), fallIn()]
})
export class EmailComponent implements OnInit {

  state: '';
  error: any;

  // constructor(public af: AngularFire, private router: Router) {
  //   this.af.auth.subscribe(auth => {
  //     if (auth) {
  //       this.router.navigateByUrl('/members');
  //     }
  //   });
  // }
  constructor(private router: Router) {}

  @HostBinding('@moveIn') role = '';

  onSubmit(formData) {
    // if (formData.valid) {
    //   console.log(formData.value);
    //   this.af.auth.login({
    //     email: formData.value.email,
    //     password: formData.value.password
    //   },
    //   {
    //     provider: AuthProviders.Password,
    //     method: AuthMethods.Password,
    //   }).then(
    //     (success) => {
    //     console.log(success);
    //     this.router.navigate(['/members']);
    //   }).catch(
    //     (err) => {
    //     console.log(err);
    //     this.error = err;
    //   });
    // }
  }

  ngOnInit() {
  }

}
