import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UserAccountService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getBankAccount(userId: string) {
    return this.apiUrl + '/account/' + userId;
  }
}
