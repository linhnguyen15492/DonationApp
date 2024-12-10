import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PhoneNumberComponent } from './phone-number/phone-number.component';
import { CodeComponent } from './code/code.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { WeatherComponent } from './weather/weather.component';

const routes: Routes = [
  { path: 'phone', component: PhoneNumberComponent },
  { path: 'code', component: CodeComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: '', redirectTo: '/phone', pathMatch: 'full' },
  { path: 'weather', component: WeatherComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
