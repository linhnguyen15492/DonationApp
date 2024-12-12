import { Component, Inject, Injector, OnInit, Type } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { CampaignService } from '../campaign.service';
import { Campaign } from 'src/app/models/campaign';
import { CommonModule, NgClass, NgIf } from '@angular/common';
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
        Bạn có chắc chắn <span class="fw-bold">{{ title }}</span> chiến dịch
        <span class="fw-bold text-primary">{{ campagin.name }}</span
        >?
      </p>
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
  title: string = '';
  constructor(
    public modal: NgbActiveModal,
    @Inject('campaign') campaign: Campaign,
    @Inject('tittle') tittle: string
  ) {
    this.campagin = campaign;
    this.title = tittle;
  }

  onchange() {
    console.log();
  }
}

const MODALS: { [name: string]: Type<any> } = {
  confirmModal: NgModalConfirm,
};

@Component({
  selector: 'app-campaign-list',
  imports: [RouterModule, CommonModule, NgIf],
  templateUrl: './campaign-list.component.html',
  styleUrl: './campaign-list.component.css',
})
export class CampaignListComponent implements OnInit {
  user: User | null = null;
  campaigns: Campaign[] = [];

  constructor(
    private authService: AuthService,
    private router: Router,
    private campaignService: CampaignService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
    this.user = this.authService.getUser();

    if (!this.user) {
      this.router.navigate(['/login']);
    }

    if (this.user && this.user.roles !== 'CharitableOrganization') {
      this.router.navigate(['/unauthorize-error']);
    }

    this.getCampagins();

    // this.authService.currentUser.subscribe((user) => {
    //   this.user = user;

    //   if (!this.user) {
    //     this.router.navigate(['/login']);
    //   }

    //   if (this.user && this.user.roles !== 'CharitableOrganization') {
    //     this.router.navigate(['/unauthorize-error']);
    //   }

    //   this.getCampagins();

    //   // this.getAllCampaigns();
    // });
  }

  getCampagins() {
    this.campaignService
      .getCampaignByUserId(this.user!.id)
      .subscribe((campaigns) => {
        console.log(campaigns);
        this.campaigns = campaigns;
      });
  }

  getAllCampaigns() {
    this.campaignService.getCampaigns().subscribe((campaigns) => {
      this.campaigns = campaigns;

      console.log(this.campaigns);
    });
  }

  activateCampaign(campaignId: number) {
    console.log('Activating campaign', campaignId);
    this.campaignService.activateCampaign(campaignId).subscribe({
      next: (data) => {
        console.log('Campaign activated', data);
        window.location.reload();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  activateCampaignConfirmation(campagin: Campaign) {
    this.modalService
      .open(MODALS['confirmModal'], {
        ariaLabelledBy: 'modal-basic-title',
        injector: Injector.create({
          providers: [
            { provide: 'campaign', useValue: campagin },
            { provide: 'tittle', useValue: 'kích hoạt' },
          ],
        }),
      })
      .result.then(
        (result) => {
          this.activateCampaign(campagin.id);
        },
        (reason) => {}
      );
  }

  deactivateCampaign(campaignId: number) {
    console.log('Deactivating campaign', campaignId);
    this.campaignService.deactivateCampaign(campaignId).subscribe({
      next: (data) => {
        console.log('Campaign deactivated', data);
        window.location.reload();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  deactivateCampaignConfirmation(campagin: Campaign) {
    this.modalService
      .open(MODALS['confirmModal'], {
        ariaLabelledBy: 'modal-basic-title',
        injector: Injector.create({
          providers: [
            { provide: 'campaign', useValue: campagin },
            { provide: 'tittle', useValue: 'vô hiệu hóa' },
          ],
        }),
      })
      .result.then(
        (result) => {
          this.deactivateCampaign(campagin.id);
        },
        (reason) => {}
      );
  }
}
