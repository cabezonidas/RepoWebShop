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

const homeRoutes: Routes = [
  { path: 'start', component: HomeShellComponent }
];

@NgModule({
  imports: [
    ScrollToModule.forRoot(),
    RouterModule.forChild(homeRoutes),
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule
  ],
  declarations: [
    VideoComponent,
    HomeShellComponent,
    ContactComponent,
    InfoComponent,
    HoursComponent,
  ],
  providers: []
})
export class HomeModule { }
