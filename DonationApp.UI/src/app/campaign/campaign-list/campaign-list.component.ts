import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { CampaignService } from '../campaign.service';
import { Campaign } from 'src/app/models/campaign';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-campaign-list',
  imports: [RouterModule, CommonModule],
  templateUrl: './campaign-list.component.html',
  styleUrl: './campaign-list.component.css',
})
export class CampaignListComponent implements OnInit {
  user: User | null = null;
  campaigns: Campaign[] = [];

  constructor(
    private authService: AuthService,
    private router: Router,
    private campaignService: CampaignService
  ) {}

  ngOnInit() {
    this.authService.currentUser.subscribe((user) => {
      this.user = user;

      // if (!this.user) {
      //   this.router.navigate(['/login']);
      // }

      // if (this.user && this.user.roles !== 'CharitableOrganization') {
      //   this.router.navigate(['/unauthorize-error']);
      // }

      // this.getCampagins();

      this.getAllCampaigns();
    });
  }

  getCampagins() {
    this.campaignService
      .getCampaignByUserId(this.user!.id)
      .subscribe((campaigns) => {
        console.log(campaigns);
        this.campaigns = campaigns;
      });
  }

  getAllCampaigns() {
    this.campaignService.getCampaigns().subscribe((campaigns) => {
      this.campaigns = campaigns;

      console.log(this.campaigns);
    });
  }

  activateCampaign(campaignId: number) {
    console.log('Activating campaign', campaignId);
    this.campaignService.activateCampaign(campaignId).subscribe({
      next: (data) => {
        console.log('Campaign activated', data);
        window.location.reload();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  deactivateCampaign(campaignId: number) {
    console.log('Deactivating campaign', campaignId);
    this.campaignService.deactivateCampaign(campaignId).subscribe({
      next: (data) => {
        console.log('Campaign deactivated', data);
        window.location.reload();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
