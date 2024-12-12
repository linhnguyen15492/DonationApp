import { Component, Inject, Injector, Type } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { CampaignService } from '../campaign/campaign.service';
import { Subscriber } from '../models/subscriber';
import { Campaign } from '../models/campaign';
import { CommonModule } from '@angular/common';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'ng-modal-confirm',
  template: `
    <div class="modal-header">
      <h5 class="modal-title fw-bold" id="modal-title">Confirmation</h5>
      <button
        type="button"
        class="btn close"
        aria-label="Close button"
        aria-describedby="modal-title"
        (click)="modal.dismiss('Cross click')"
      >
        <span aria-hidden="true">×</span>
      </button>
    </div>
    <div class="modal-body">
      <p>
        Bạn có chắc chắn
        <span
          class="fw-bold text-success"
          [ngClass]="{ 'text-danger': title === 'từ chối' }"
          >{{ title }}</span
        >
        đăng ký của
        <span class="fw-bold text-primary">{{ subscriber.fullName }}</span>
        cho chiến dịch
        <span class="fw-bold text-primary">{{ campagin.name }}</span
        >?
      </p>
      <div class="alert alert-warning">
        <p class="text-danger">
          Trước khi xác nhận, hãy đảm bảo bạn đã kiểm tra kỹ thông tin của người
          dùng và xác nhận rằng họ đã cung cấp đầy đủ dữ liệu theo yêu cầu của
          chiến dịch cũng như quy định pháp luật.
          <br />
          Đồng thời, hãy tuân thủ nghiêm ngặt quy trình xét duyệt của chiến dịch
          và các quy định liên quan khi thực hiện xác thực thông tin của người
          đăng ký.
        </p>
      </div>
    </div>

    <div class="modal-footer">
      <button
        type="button"
        class="btn btn-outline-secondary"
        (click)="modal.dismiss('cancel click')"
      >
        Hủy
      </button>
      <button
        type="button"
        ngbAutofocus
        class="btn btn-success"
        (click)="modal.close('Ok click')"
      >
        Xác nhận
      </button>
    </div>
  `,
  standalone: true,
  imports: [CommonModule],
})
export class NgModalConfirm {
  campagin: Campaign = {} as Campaign;
  subscriber: Subscriber = {} as Subscriber;
  title: string = '';
  constructor(
    public modal: NgbActiveModal,
    @Inject('campaign') campaign: Campaign,
    @Inject('subscriber') subscriber: Subscriber,
    @Inject('tittle') tittle: string
  ) {
    this.campagin = campaign;
    this.title = tittle;
    this.subscriber = subscriber;
  }

  onchange() {
    console.log();
  }
}

const MODALS: { [name: string]: Type<any> } = {
  confirmModal: NgModalConfirm,
};

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
    private router: Router,
    private modalService: NgbModal
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

  approve(campaign: Campaign, subscriber: Subscriber) {
    this.campaignService
      .approveSubscriber(subscriber.userId, campaign.id)
      .subscribe({
        next: (data) => {
          console.log('Approve subscriber', data);
          window.location.reload();
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  approveConfirmation(campagin: Campaign, subscriber: Subscriber) {
    this.modalService
      .open(MODALS['confirmModal'], {
        ariaLabelledBy: 'modal-basic-title',
        injector: Injector.create({
          providers: [
            { provide: 'campaign', useValue: campagin },
            { provide: 'subscriber', useValue: subscriber },
            { provide: 'tittle', useValue: 'chấp nhận' },
          ],
        }),
      })
      .result.then(
        (result) => {
          this.approve(campagin, subscriber);
        },
        (reason) => {}
      );
  }

  reject(campaign: Campaign, subscriber: Subscriber) {
    this.campaignService
      .rejectSubscriber(subscriber.userId, campaign.id)
      .subscribe({
        next: (data) => {
          console.log('Reject subscriber', data);
          window.location.reload();
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  rejectConfirmation(campagin: Campaign, subscriber: Subscriber) {
    this.modalService
      .open(MODALS['confirmModal'], {
        ariaLabelledBy: 'modal-basic-title',
        injector: Injector.create({
          providers: [
            { provide: 'campaign', useValue: campagin },
            { provide: 'subscriber', useValue: subscriber },
            { provide: 'tittle', useValue: 'từ chối' },
          ],
        }),
      })
      .result.then(
        (result) => {
          this.reject(campagin, subscriber);
        },
        (reason) => {}
      );
  }
}
