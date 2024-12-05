import { Injectable } from '@angular/core';
import { Campaign } from '../models/campaign';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { TransferModel } from '../models/transferModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TransferManagerService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  transfer(transferModel: TransferModel) {
    return this.http.post(`${this.apiUrl}/transfer/mock-donate`, transferModel);
  }
}
