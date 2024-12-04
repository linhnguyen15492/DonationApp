import { Component, OnInit, Type } from '@angular/core';
import { Router } from '@angular/router';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CampaignService } from './campaign.service';
import { HttpClient } from '@angular/common/http';
import { computeStyles } from '@popperjs/core';
import { Campaign } from '../models/campaign';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'ng-modal-confirm',
  template: `
    <div class="modal-header">
      <h5 class="modal-title" id="modal-title">Delete Confirmation</h5>
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
      <p>Are you sure you want to delete?</p>
    </div>
    <div class="modal-footer">
      <button
        type="button"
        class="btn btn-outline-secondary"
        (click)="modal.dismiss('cancel click')"
      >
        CANCEL
      </button>
      <button
        type="button"
        ngbAutofocus
        class="btn btn-success"
        (click)="modal.close('Ok click')"
      >
        OK
      </button>
    </div>
  `,
})
export class NgModalConfirm {
  constructor(public modal: NgbActiveModal) { }
}

const MODALS: { [name: string]: Type<any> } = {
  deleteModal: NgModalConfirm,
};

@Component({
  selector: 'app-campaign',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './campaign.component.html',
  styleUrl: './campaign.component.css',
})
export class CampaignComponent implements OnInit {
  closeResult = '';
  campaigns: Campaign[] = [];
  constructor(
    private router: Router,
    private modalService: NgbModal,
    // private toastr: ToastrService,
    private service: CampaignService,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.getCampaigns();
  }

  async getCampaigns() {
    await this.service.getCampaigns().subscribe((data) => {
      console.log(data);
      this.campaigns = data;
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
