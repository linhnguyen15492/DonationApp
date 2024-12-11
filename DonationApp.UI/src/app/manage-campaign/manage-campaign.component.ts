import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-manage-campaign',
  imports: [],
  templateUrl: './manage-campaign.component.html',
  styleUrl: './manage-campaign.component.css',
})
export class ManageCampaignComponent {
  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      console.log(params);
    });
  }
}
