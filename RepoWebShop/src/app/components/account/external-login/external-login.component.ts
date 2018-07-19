import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../services/account.service';
import { ImagesService } from '../../../services/images.service';

@Component({
  selector: 'app-external-login',
  templateUrl: './external-login.component.html',
  styleUrls: ['./external-login.component.scss']
})
export class ExternalLoginComponent implements OnInit {

  constructor(private account: AccountService) { }

  providers: Array<string> = [];

  ngOnInit() {
    this.account.externalLoginProviders().subscribe(providers => this.providers = providers);


  }
  remoteLogin(provider: string): void {
    console.log('test');
    this.account.externalLogin(provider).subscribe((data) => console.log(data));
  }
}
/*
    apiKey: "AIzaSyBmyyAkUbYPLALlmPzgSoUcig5scZSexZA",
    authDomain: "de-las-artes.firebaseapp.com",
    databaseURL: "https://de-las-artes.firebaseio.com",
    projectId: "de-las-artes",
    storageBucket: "de-las-artes.appspot.com",
    messagingSenderId: "354698240101"
*/
