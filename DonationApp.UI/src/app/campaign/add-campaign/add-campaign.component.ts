import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateCampaign } from 'src/app/models/campaign';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { CampaignService } from '../campaign.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-campaign',
  imports: [CommonModule, FormsModule],
  templateUrl: './add-campaign.component.html',
  styleUrl: './add-campaign.component.css',
})
export class AddCampaignComponent {
  campaign: CreateCampaign = {} as CreateCampaign;
  user: User | null = null;

  constructor(
    private authService: AuthService,
    private campaignService: CampaignService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.currentUser.subscribe((user) => {
      this.user = user;
    });
  }

  createCampaign() {
    this.campaign.organizationId = this.user!.id;

    setTimeout(() => {
      this.campaignService.addCampaign(this.campaign).subscribe((campaign) => {
        console.log(campaign);

        this.router.navigate(['/campaignManager']);
      });
    }, 1000);

    console.log(this.campaign);
  }
}
