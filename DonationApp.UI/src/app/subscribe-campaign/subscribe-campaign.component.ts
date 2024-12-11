import { Component } from '@angular/core';
import { CampaignService } from '../campaign/campaign.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { User } from '../models/user';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-subscribe-campaign',
  imports: [CommonModule],
  templateUrl: './subscribe-campaign.component.html',
  styleUrl: './subscribe-campaign.component.css',
})
export class SubscribeCampaignComponent {
  constructor(
    private campaginService: CampaignService,
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService
  ) {}

  campaignId: number = 0;
  user: User | null = null;
  subscribed: boolean = false;
  isLoggedIn: boolean = false;

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.campaignId = params['id'];
    });

    this.authService.currentUser.subscribe((user) => {
      this.user = user;
      this.isSubscribed();
    });

    this.authService.isLoggedIn$.subscribe({
      next: (isLoggedIn) => {
        this.isLoggedIn = isLoggedIn;
      },
    });
  }

  onSubscribe() {
    this.campaginService
      .subscribeCampaign(this.campaignId, this.user!.id)
      .subscribe({
        next: (data) => {
          console.log(data);
          this.subscribed = true;
          this.router.navigate(['/subscribe-campaign'], {
            queryParams: { id: this.campaignId },
          });
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  isSubscribed() {
    this.campaginService
      .isSubscribed(this.campaignId, this.user!.id)
      .subscribe({
        next: (data) => {
          console.log(data);

          this.subscribed = data;
        },
        error: (error) => {
          console.log(error);
        },
      });
  }
}
