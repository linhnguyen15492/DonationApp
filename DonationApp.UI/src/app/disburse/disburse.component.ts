import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Campaign } from '../models/campaign';
import { Subscriber } from '../models/subscriber';
import { FormsModule } from '@angular/forms';
import { ErrorMessageComponent } from '../error-message/error-message.component';
import { TransferManagerService } from '../services/transfer-manager.service';
import { TransferModel } from '../models/transferModel';

@Component({
  selector: 'app-disburse',
  imports: [CommonModule, FormsModule, ErrorMessageComponent],
  templateUrl: './disburse.component.html',
  styleUrl: './disburse.component.css',
})
export class DisburseComponent implements OnInit {
  campaign: Campaign = {} as Campaign;
  subscriber: Subscriber = {} as Subscriber;
  errorMessage = '';
  transferModel: TransferModel = {} as TransferModel;
  error: boolean = false;

  constructor(
    private transferManager: TransferManagerService,
    private router: Router
  ) {
    let navigation = this.router.getCurrentNavigation();

    if (navigation && navigation.extras.state) {
      let model = navigation.extras.state['model'];
      console.log('model', model);
      this.campaign = model.campaign;
      this.subscriber = model.subscriber;
    }
  }

  ngOnInit(): void {
    this.transferModel.fromAccountNumber = this.campaign.accountNumber;
    this.transferModel.toAccountNumber = this.subscriber.accountNumber;
    this.transferModel.sender = this.campaign.name;
    this.transferModel.receiver = this.subscriber.fullName;
    this.transferModel.transferType = 1;
  }

  onSubmit() {
    console.log('transferModel', this.transferModel);

    this.transferManager.disburse(this.transferModel).subscribe({
      next: (result) => {
        console.log('result', result);

        this.router.navigate(['/transfer-result'], {
          queryParams: {
            amount: this.transferModel.amount,
            note: this.transferModel.note,
            success: result.isSuccess,
            sender: result.sender,
            receiver: result.receiver,
          },
        });
      },
      error: (error) => {
        console.log('result', error);

        this.router.navigate(['/transfer-result'], {
          queryParams: {
            amount: this.transferModel.amount,
            note: this.transferModel.note,
            success: false,
            sender: this.transferModel.sender,
            receiver: this.transferModel.receiver,
          },
        });
      },
    });
  }

  onChangeAmount(event: any) {
    console.log(event.target.value);

    if (event.target.value > this.campaign.accountBalance) {
      this.errorMessage = 'Số dư không đủ';
      this.error = true;
    }
  }
}
