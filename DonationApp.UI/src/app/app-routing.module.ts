import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CampaignComponent } from './campaign/campaign.component';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { AboutComponent } from './about/about.component';
import { HeatmapComponent } from './heatmap/heatmap.component';
import { TransferResultComponent } from './transfer-result/transfer-result.component';
import { PhoneNumberComponent } from './phone-number/phone-number.component';
import { CodeComponent } from './code/code.component';
import { RegisterResultComponent } from './register-result/register-result.component';
import { CampaignDetailComponent } from './campaign/campaign-detail/campaign-detail.component';
import { CampaignListComponent } from './campaign/campaign-list/campaign-list.component';
import { UnauthorizedErrorComponent } from './unauthorized-error/unauthorized-error.component';
import { AddCampaignComponent } from './campaign/add-campaign/add-campaign.component';
import { SubscribeCampaignComponent } from './subscribe-campaign/subscribe-campaign.component';
import { ManageCampaignComponent } from './manage-campaign/manage-campaign.component';
import { DisburseComponent } from './disburse/disburse.component';
import { CharityDocumentsComponent } from './charity-documents/charity-documents.component';

const routes: Routes = [
  { path: 'campaign', component: CampaignComponent },
  { path: 'home', component: HomeComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'login', component: LoginComponent },
  { path: 'about', component: AboutComponent },
  { path: 'heatmap', component: HeatmapComponent },
  { path: 'transfer-result', component: TransferResultComponent },
  { path: 'register-result', component: RegisterResultComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'phone', component: PhoneNumberComponent },
  { path: 'code', component: CodeComponent },
  { path: 'campaign/:id', component: CampaignDetailComponent },
  { path: 'campaignManager', component: CampaignListComponent },
  { path: 'unauthorize-error', component: UnauthorizedErrorComponent },
  { path: 'add-campaign', component: AddCampaignComponent },
  { path: 'subscribe-campaign', component: SubscribeCampaignComponent },
  { path: 'manage-campaign/:id', component: ManageCampaignComponent },
  { path: 'disburse', component: DisburseComponent },
  { path: 'charity-document', component: CharityDocumentsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
