import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { IAppUser } from '../interfaces/iapp-user';

@Injectable({
  providedIn: 'root'
})

// export class AuthGuard implements CanActivate {

//     constructor(private auth: AngularFireAuth, private router: Router) { }

//     canActivate(): Observable<boolean> {
//       return this.auth.user
//         .map(state => !!state)
//         .do(authenticated => {
//           if (!authenticated) {
//             this.router.navigate([ '/login' ]);
//         }});
//     }
// }
export class AuthService {

  private userSource = new BehaviorSubject<IAppUser>({

  } as IAppUser);
  public currentUser = this.userSource.asObservable();

  constructor(private http: HttpClient) { }

  getUser = () =>
    (this.http.get('/api/_account/current') as Observable<IAppUser>)
      .subscribe((user) => this.userSource.next(user))

  isAdmin = (): Observable<boolean> => {
    return (this.http.get('/api/_account/isAdmin') as Observable<boolean>);
  }

}





