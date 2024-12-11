import { Component } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { CampaignService } from '../campaign/campaign.service';
import { Subscriber } from '../models/subscriber';
import { Campaign } from '../models/campaign';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-campaign',
  imports: [CommonModule],
  templateUrl: './manage-campaign.component.html',
  styleUrl: './manage-campaign.component.css',
})
export class ManageCampaignComponent {
  constructor(
    private route: ActivatedRoute,
    private campaignService: CampaignService,
    private router: Router
  ) {}

  subscribers: Subscriber[] = [];
  campaignId: number = 0;
  campaign: Campaign = {} as Campaign;

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.campaignId = params['id'];
      console.log('params', this.campaignId);
    });

    this.campaignService
      .getSubscribers(this.campaignId)
      .subscribe((subscribers) => {
        this.subscribers = subscribers;

        console.log('subscribers', this.subscribers);
      });

    this.campaignService.getCampaign(this.campaignId).subscribe((campaign) => {
      this.campaign = campaign;
      console.log('campaign', this.campaign);
    });
  }

  disburse(campaign: Campaign, subscriber: Subscriber) {
    console.log('Disbursing for subscribers', this.subscribers);

    let model = { campaign: campaign, subscriber: subscriber };

    let user = { name: 'Raja', age: 20, email: 'raja@mail.com' };
    // let navigationExtras: NavigationExtras = {
    //   state: {
    //     user: user,
    //   },
    // };

    let navigationExtras: NavigationExtras = {
      state: {
        model: model,
      },
    };

    this.router.navigate(['/disburse'], navigationExtras);
  }
}
