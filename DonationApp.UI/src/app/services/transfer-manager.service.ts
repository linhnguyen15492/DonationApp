import { Injectable } from '@angular/core';
import { Campaign } from '../models/campaign';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { TransferModel } from '../models/transferModel';
import { Observable } from 'rxjs';
import { TransactionResult } from '../models/transactionResult';

@Injectable({
  providedIn: 'root',
})
export class TransferManagerService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  transfer(transferModel: TransferModel): Observable<TransactionResult> {
    console.log('transferModel trong service', transferModel);
    return this.http.post<TransactionResult>(
      `${this.apiUrl}/transfer/donate`,
      transferModel
    );
  }

  disburse(transferModel: TransferModel): Observable<TransactionResult> {
    console.log('transferModel trong service', transferModel);
    return this.http.post<TransactionResult>(
      `${this.apiUrl}/transfer/disburse`,
      transferModel
    );
  }
}
