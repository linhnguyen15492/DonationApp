import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ApiPaths } from '../api-paths';
import { Campaign } from '../models/campaign';
import { MessageService } from '../services/message.service';

@Injectable({
  providedIn: 'root',
})
export class CampaignService {
  private campaignUrl = {
    getAllCampaigns:
      environment.apiUrl + ApiPaths.Campaign + '/get-all-campaigns',
    addCampaign: environment.apiUrl + ApiPaths.Campaign + '/create-campaign/',
    getCampaignById: environment.apiUrl + '/api/campaign/',
    deleteCampaignById: environment.apiUrl + '/api/campaign/',
    updateCampaign: environment.apiUrl + '/api/campaign/',
  };

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) { }

  private log(message: string) {
    this.messageService.add(`HeroService: ${message}`);
  }

  getCampaigns(): Observable<Campaign[]> {
    return this.http.get<Campaign[]>(this.campaignUrl.getAllCampaigns).pipe(
      tap((_) => this.log('fetched campaigns')),
      catchError(this.handleError<Campaign[]>('getCampaigns', []))
    );
  }

  getCampaign(id: number): Observable<Campaign> {
    const url = `${this.campaignUrl.getCampaignById}/${id}`;
    return this.http.get<Campaign>(url).pipe(
      tap((_) => this.log(`fetched campaign id=${id}`)),
      catchError(this.handleError<Campaign>(`getCampaign id=${id}`))
    );
  }

  updateCampaign(campaign: Campaign): Observable<any> {
    return this.http
      .put(this.campaignUrl.updateCampaign, campaign, this.httpOptions)
      .pipe(
        tap((_) => this.log(`updated campaign id=${campaign.id}`)),
        catchError(this.handleError<any>('updateCampaign'))
      );
  }

  addCampaign(campaign: Campaign): Observable<Campaign> {
    return this.http
      .post<Campaign>(this.campaignUrl.addCampaign, campaign, this.httpOptions)
      .pipe(
        tap((newCampaign: Campaign) =>
          this.log(`added hero w/ id=${newCampaign.id}`)
        ),
        catchError(this.handleError<Campaign>('addCampaign'))
      );
  }

  deleteCampaign(id: number): Observable<Campaign> {
    const url = `${this.campaignUrl.deleteCampaignById}/${id}`;

    return this.http.delete<Campaign>(url, this.httpOptions).pipe(
      tap((_) => this.log(`deleted campaign id=${id}`)),
      catchError(this.handleError<Campaign>('deleteCampaign'))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
