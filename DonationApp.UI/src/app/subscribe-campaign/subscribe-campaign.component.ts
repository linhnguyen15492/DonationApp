import { Component } from '@angular/core';

@Component({
  selector: 'app-subscribe-campaign',
  imports: [],
  templateUrl: './subscribe-campaign.component.html',
  styleUrl: './subscribe-campaign.component.css',
})
export class SubscribeCampaignComponent {
  constructor() {}

  ngOnInit() {}

  onSubscribe() {
    console.log('Subscribe button clicked!');
  }
}
