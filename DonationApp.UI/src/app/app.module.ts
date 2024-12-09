import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HTTP_INTERCEPTORS, provideHttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NotificationComponent } from './notification/notification.component';
import { SignupComponent } from './signup/signup.component';
import { CommonModule } from '@angular/common';
import { ActorFormComponent } from './actor-form/actor-form.component';
import { CampaignDetailComponent } from './campaign/campaign-detail/campaign-detail.component';
import { AuthInterceptor } from './services/authInterceptor';
import { HeatmapComponent } from './heatmap/heatmap.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [AppComponent, ActorFormComponent],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    RouterModule,
    NotificationComponent,
    SignupComponent,
    CampaignDetailComponent,
    HeatmapComponent,
    NgbModule
  ],
  providers: [provideHttpClient(),
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
