import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { EmailComponent } from './components/email/email.component';
import { LoginComponent } from './components/login/login.component';
import { MembersComponent } from './components/members/members.component';
import { MobileComponent } from './components/mobile/mobile.component';
import { PasswordComponent } from './components/password/password.component';
import { SignupComponent } from './components/signup/signup.component';
import { AngularFireModule } from 'angularfire2';
import { AngularFireAuthModule } from 'angularfire2/auth';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AuthService } from './services/auth.service';
import { SmsService } from './services/sms.service';
import { environment } from '../../environments/environment';
import { AuthGuardService } from '../core/services/guard/auth-guard.service';
import { SharedModule } from '../shared/shared.module';

const authRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'email', component: EmailComponent },
  { path: 'profile', component: MembersComponent, canActivate: [AuthGuardService] },
  { path: 'mobile', component: MobileComponent, canActivate: [AuthGuardService] },
  { path: 'password', component: PasswordComponent },
  { path: 'signup', component: SignupComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(authRoutes),
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AngularFireModule.initializeApp(environment.firebase), AngularFireAuthModule,
    MaterialModule, FormsModule, ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    EmailComponent,
    LoginComponent,
    MembersComponent,
    MobileComponent,
    PasswordComponent,
    SignupComponent
  ],
  providers: [
    AuthService, SmsService
  ]
})
export class AuthenticationModule { }
