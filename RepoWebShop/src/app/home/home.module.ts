import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { VideoComponent } from './components/video/video.component';
import { HomeShellComponent } from './containers/home-shell/home-shell.component';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { ContactComponent } from './components/contact/contact.component';
import { InfoComponent } from './components/info/info.component';
import { ScrollToModule } from '@nicky-lenaers/ngx-scroll-to';
import { HoursComponent } from './components/hours/hours.component';
import { ProductModule } from '../products/products.module';
import { SharedModule } from '../shared/shared.module';
import { CateringModule } from '../catering/catering.module';
import { MatDialogModule } from '@angular/material';
import { WelcomeDialogComponent } from './components/welcome-dialog/welcome-dialog.component';

const homeRoutes: Routes = [
  { path: 'start', component: HomeShellComponent },
  { path: '', component: HomeShellComponent }
];

@NgModule({
  imports: [
    ScrollToModule.forRoot(),
    RouterModule.forChild(homeRoutes),
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule,
    SharedModule,
    ProductModule,
    CateringModule,
    MatDialogModule,
  ],
  declarations: [
    VideoComponent,
    HomeShellComponent,
    ContactComponent,
    InfoComponent,
    HoursComponent,
    WelcomeDialogComponent,
  ],
  providers: [],
  entryComponents: [WelcomeDialogComponent]
})
export class HomeModule { }
