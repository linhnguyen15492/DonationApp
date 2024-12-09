import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Campaign } from 'src/app/models/campaign';
import { CampaignService } from '../campaign.service';

@Component({
  selector: 'app-campaign-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './campaign-detail.component.html',
  styleUrl: './campaign-detail.component.css',
})
export class CampaignDetailComponent implements OnInit {
  campaign: any;

  constructor(
    private route: ActivatedRoute,
    private campaignService: CampaignService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.campaignService.getCampaign(params['id']).subscribe({
        next: (data) => {
          this.campaign = {
            ...data,
            comments: data.comments.map((c: any) => ({
              userName: c.userName,
              content: c.content,
            })),
          };
        },
      });
    });
  }
}
