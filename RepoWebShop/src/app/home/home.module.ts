import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { NgModule } from '@angular/core';
import { MaterialModule } from '../material/material.module';
import { VideoComponent } from './components/video/video.component';
import { HomeShellComponent } from './containers/home-shell/home-shell.component';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

const homeRoutes: Routes = [
  { path: 'start', component: HomeShellComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(homeRoutes),
    CommonModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule
  ],
  declarations: [
    VideoComponent,
    HomeShellComponent,
  ],
  providers: []
})
export class HomeModule { }
