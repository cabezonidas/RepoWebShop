import { NgModule } from '@angular/core';
import { MobileGuardService } from './services/guard/mobile-guard.service';
import { AdminGuardService } from './services/guard/admin-guard.service';
import { AuthGuardService } from './services/guard/auth-guard.service';
import { AppService } from './services/app/app.service';

@NgModule({
    providers: [
        AdminGuardService, AuthGuardService, MobileGuardService, AppService
    ]
})

export class CoreModule { }
