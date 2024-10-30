import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CampaignComponent } from './campaign/campaign.component';
import { ShowCampaignComponent } from './campaign/show-campaign/show-campaign.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HeroesComponent } from './heroes/heroes.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateCampaignComponent } from './campaign/create-campaign/create-campaign.component';
import { EditCampaignComponent } from './campaign/edit-campaign/edit-campaign.component';
import { DeleteCampaignComponent } from './campaign/delete-campaign/delete-campaign.component';
import { HomeComponent } from './home/home.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { ViewEmployeeComponent } from './view-employee/view-employee.component';

@NgModule({
  declarations: [
    AppComponent,
    CampaignComponent,
    ShowCampaignComponent,
    HeroesComponent,
    DashboardComponent,
    CreateCampaignComponent,
    EditCampaignComponent,
    DeleteCampaignComponent,
    HomeComponent,
    AddEmployeeComponent,
    EditEmployeeComponent,
    ViewEmployeeComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}