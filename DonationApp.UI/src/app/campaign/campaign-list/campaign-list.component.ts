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

      if (!this.user) {
        this.router.navigate(['/login']);
      }

      if (this.user && this.user.roles !== 'CharitableOrganization') {
        this.router.navigate(['/unauthorize-error']);
      }

      this.getCampagins();
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
}
