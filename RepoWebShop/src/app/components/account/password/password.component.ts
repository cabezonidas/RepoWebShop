import { Component, OnInit, HostBinding } from '@angular/core';
import { moveIn } from '../../../animations/router.animations';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-password',
  templateUrl: './password.component.html',
  styleUrls: ['./password.component.scss'],
  animations: [moveIn()]
})
export class PasswordComponent implements OnInit {

  constructor(private auth: AuthService) { }
  @HostBinding('@moveIn') role = '';

  ngOnInit() {
  }

}
