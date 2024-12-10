import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Campaign } from 'src/app/models/campaign';
import { CampaignService } from '../campaign.service';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { CommentModel, CommentResponse } from 'src/app/models/comment';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-campaign-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './campaign-detail.component.html',
  styleUrl: './campaign-detail.component.css',
})
export class CampaignDetailComponent implements OnInit {
  campaign: Campaign = {} as Campaign;
  user: User | null = null;
  isLoggedIn: boolean = false;

  commentContent: string = '';

  isLiked: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private campaignService: CampaignService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.campaignService.getCampaign(params['id']).subscribe({
        next: (data: Campaign) => {
          this.campaign = {
            ...data,
            comments: data.comments.map((c: CommentResponse) => ({
              userName: c.userName,
              content: c.content,
              userId: c.userId,
              campaignId: c.campaignId,
            })),
          };

          if (this.isLoggedIn) {
            this.isLikedCampaign();
          }
        },

        error: (error) => {
          console.log(error);
        },
      });
    });

    this.authService.isLoggedIn$.subscribe({
      next: (isLoggedIn) => {
        this.isLoggedIn = isLoggedIn;
      },
    });

    this.authService.currentUser.subscribe({
      next: (user) => {
        this.user = user;
      },
    });
  }

  comment() {
    const comment: CommentModel = {
      content: this.commentContent,
      campaignId: this.campaign.id,
      userId: this.user!.id,
    };

    console.log(comment);

    this.campaignService.comment(comment).subscribe({
      next: (data) => {
        console.log(data);
        this.commentContent = '';
        this.campaignService.getCampaign(this.campaign.id).subscribe({
          next: (data) => {
            this.campaign = data;
          },
        });
      },
    });
  }

  onclick() {
    console.log(this.isLiked);

    // this.isLiked = !this.isLiked;

    if (!this.isLiked) {
      console.log('goi like');
      this.campaignService.like(this.campaign.id, this.user!.id).subscribe({
        next: (data) => {
          this.campaignService.getCampaign(this.campaign.id).subscribe({
            next: (data) => {
              this.campaign = data;
            },
          });
        },
      });
    }

    if (this.isLiked) {
      console.log('goi unlike');
      this.campaignService.unlike(this.campaign.id, this.user!.id).subscribe({
        next: (data) => {
          this.campaignService.getCampaign(this.campaign.id).subscribe({
            next: (data) => {
              this.campaign = data;
            },
          });
        },
      });
    }

    this.isLiked = !this.isLiked;
  }

  like() {
    this.campaignService.like(this.campaign.id, this.user!.id).subscribe({
      next: (data) => {
        this.campaignService.getCampaign(this.campaign.id).subscribe({
          next: (data) => {
            this.campaign = data;
          },
        });
      },
    });
  }

  unlike() {
    this.campaignService.like(this.campaign.id, this.user!.id).subscribe({
      next: (data) => {
        this.campaignService.getCampaign(this.campaign.id).subscribe({
          next: (data) => {
            this.campaign = data;
          },
        });
      },
    });
  }

  isLikedCampaign() {
    this.campaignService.isLiked(this.campaign.id, this.user!.id).subscribe({
      next: (data) => {
        console.log(data);
        this.isLiked = data;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
