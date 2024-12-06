import { Component, OnInit, Type, Inject, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CampaignService } from './campaign.service';
import { HttpClient } from '@angular/common/http';
import { Campaign } from '../models/campaign';
import { CommonModule } from '@angular/common';
import { TransferManagerService } from '../services/transfer-manager.service';
import { AuthService } from '../services/auth.service';
import { TransferModel } from '../models/transferModel';
import { User } from '../models/user';
import { TransferResult } from '../models/transferResult';

@Component({
  selector: 'ng-modal-confirm',
  template: `
    <div class="modal-header">
      <h5 class="modal-title" id="modal-title">Xác nhận đóng góp</h5>
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
        Bạn có muốn đóng góp cho chương trình
        <span class="fw-bold text-success"
          >{{ campaign.name }} {{ campaign.accountNumber }}</span
        >?
      </p>
      <input
        type="number"
        class="form-control mb-2"
        name="amount"
        #amount
        placeholder="Nhập số tiền"
        required
      />
      <textarea
        type="text"
        class="form-control mb-2"
        name="note"
        #note
        placeholder="Nhập lời nhắn"
        rows="5"
      ></textarea>
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
        [disabled]="!amount.value || +amount.value === 0"
        type="button"
        ngbAutofocus
        class="btn btn-success"
        (click)="
          modal.close({
            amount: amount.value,
            note: note.value,
            toAccount: campaign.accountNumber
          })
        "
      >
        Xác nhận
      </button>
    </div>
  `,
  standalone: true,
})
export class NgModalConfirm {
  campaign: Campaign;
  constructor(
    public modal: NgbActiveModal,
    @Inject('campaign') campaign: Campaign
  ) {
    this.campaign = campaign;
  }
}

const MODALS: { [name: string]: Type<any> } = {
  donateModal: NgModalConfirm,
};

@Component({
  selector: 'app-campaign',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './campaign.component.html',
  styleUrl: './campaign.component.css',
})
export class CampaignComponent implements OnInit {
  closeResult = '';
  campaigns: Campaign[] = [];

  user: User | null = null;

  constructor(
    private router: Router,
    private modalService: NgbModal,
    private service: CampaignService,
    private http: HttpClient,
    private transferManager: TransferManagerService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.getCampaigns();
  }

  async getCampaigns() {
    await this.service.getCampaigns().subscribe({
      next: (data) => {
        console.log(data);
        this.campaigns = data;
      },
    });
  }

  AddCampaign() {
    this.router.navigate(['AddEmployee']);
  }

  add(name: string): void {
    name = name.trim();
    if (!name) {
      return;
    }
    this.service.addCampaign({ name } as Campaign).subscribe((campaign) => {
      this.campaigns.push(campaign);
    });
  }

  delete(campaign: Campaign): void {
    this.campaigns = this.campaigns.filter((c) => c !== campaign);
    this.service.deleteCampaign(campaign.id).subscribe((res) => {
      console.log(res);
    });
  }

  donate(campaign: Campaign) {
    this.authService.isLoggedIn$.subscribe({
      next: (isLoggedIn) => {
        if (!isLoggedIn) {
          this.router.navigate(['/login']);
          return;
        }

        this.authService.currentUser.subscribe({
          next: (user) => {
            this.user = user;
            console.log(this.user);
          },
        });

        this.modalService
          .open(MODALS['donateModal'], {
            ariaLabelledBy: 'modal-basic-title',
            injector: Injector.create({
              providers: [{ provide: 'campaign', useValue: campaign }],
            }),
          })
          .result.then((result) => {
            console.log('result lấy từ modal, user, campaign trong component', result);
            const transferModel: TransferModel = {
              fromAccountNumber: this.user!.accountNumber,
              toAccountNumber: campaign.accountNumber,
              amount: result.amount,
              note: result.note,
              type: 0,
              sender: this.user!.fullName,
              receiver: campaign.name,
            };
            this.transferManager.transfer(transferModel).subscribe({
              next: (res) => {
                console.log('res trong component', res);
                this.router.navigate(['/transfer-result'], {
                  queryParams: {
                    amount: result.amount,
                    note: result.note,
                    success: res.isSuccess,
                    sender: res.sender,
                    receiver: res.receiver,
                  },
                });
              },
              error: (error) => {
                console.log('error trong component', error);

                this.router.navigate(['/transfer-result'], {
                  queryParams: {
                    amount: result.amount,
                    note: result.note,
                    success: false,
                    sender: this.user!.fullName,
                    receiver: campaign.name,
                  },
                });
              },
            });
          });
      },
    });
  }

  // deleteCampaignConfirmation(employee: any) {
  //   this.modalService
  //     .open(MODALS['deleteModal'], {
  //       ariaLabelledBy: 'modal-basic-title',
  //     })
  //     .result.then(
  //       (result) => {
  //         this.deleteCampaign(employee);
  //       },
  //       (reason) => {}
  //     );
  // }

  // deleteCampaign(employee: any) {
  //   this.service.deleteCampaignById(employee.id).subscribe(
  //     (data: any) => {
  //       if (data != null && data.body != null) {
  //         var resultData = data.body;
  //         if (resultData != null && resultData.isSuccess) {
  //           // this.toastr.success(resultData.message);
  //           this.getAllCampaigns();
  //         }
  //       }
  //     },
  //     (error: any) => {}
  //   );
  // }
}
