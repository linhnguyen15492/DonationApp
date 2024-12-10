import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubscribeCampaignComponent } from './subscribe-campaign.component';

describe('SubscribeCampaignComponent', () => {
  let component: SubscribeCampaignComponent;
  let fixture: ComponentFixture<SubscribeCampaignComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubscribeCampaignComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubscribeCampaignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
