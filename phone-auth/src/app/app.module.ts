import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PhoneNumberComponent } from './phone-number/phone-number.component';
import { CodeComponent } from './code/code.component';
import { DashboardComponent } from './dashboard/dashboard.component';

import { FormsModule } from '@angular/forms';

import { AngularFireAuthModule } from "@angular/fire/compat/auth";
import { AngularFireModule } from '@angular/fire/compat';
import { AngularFirestoreModule } from '@angular/fire/compat/firestore';

import { environment } from '../environments/environment'; 
import { NgOtpInputComponent } from 'ng-otp-input';
@NgModule({
  declarations: [
    AppComponent,
    PhoneNumberComponent,
    CodeComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase),
    AngularFireAuthModule,
    AngularFirestoreModule,
    FormsModule,
    NgOtpInputComponent
  ],
  providers: [
    provideClientHydration(withEventReplay())
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
