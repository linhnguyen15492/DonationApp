import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CampaignService {
  readonly apiUrl = 'http://localhost:7112/api/';
  readonly photoUrl = 'http://localhost:50306/Photos/';

  constructor(private http: HttpClient) {}

  // Department
  getAllCampaigns(): Observable<any[]> {
    var path = 'campaign/get-all-campaigns';
    return this.http.get<any[]>(this.apiUrl + path);
  }

  createCampaign(dept: any): Observable<any> {
    var path = 'campaign/create-campaign';

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
    return this.http.post<any>(this.apiUrl + path, dept, httpOptions);
  }

  updateCampaign(dept: any): Observable<any> {
    var path = 'campaign/update-campaign';

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
    return this.http.put<any>(this.apiUrl + path, dept, httpOptions);
  }

  deleteCamapaign(deptId: number): Observable<number> {
    var path = 'campaign/delete-campaign/';

    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
    return this.http.delete<number>(this.apiUrl + path + deptId, httpOptions);
  }
}
