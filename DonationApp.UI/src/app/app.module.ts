import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { provideHttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NotificationComponent } from "./notification/notification.component";
import { SignupComponent } from "./signup/signup.component";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    RouterModule,
    NotificationComponent,
    SignupComponent
],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent],
})
export class AppModule { }
