import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { ApiPaths } from '../api-paths';
import { Campaign, CreateCampaign } from '../models/campaign';
import { MessageService } from '../services/message.service';
import { CommentModel } from '../models/comment';
import { Subscriber } from '../models/subscriber';

@Injectable({
  providedIn: 'root',
})
export class CampaignService {
  private campaignUrl = {
    getAllCampaigns:
      environment.apiUrl + ApiPaths.Campaign + '/get-all-campaigns',
    addCampaign: environment.apiUrl + ApiPaths.Campaign + '/create-campaign',
    getCampaignById: environment.apiUrl + '/campaign/get-campaign',
    deleteCampaignById: environment.apiUrl + '/campaign',
    updateCampaign: environment.apiUrl + '/campaign',
    comment: environment.apiUrl + '/campaign/add-comment',
    like: environment.apiUrl + '/campaign/like-campagin',
    unlike: environment.apiUrl + '/campaign/unlike-campagin',
    isLiked: environment.apiUrl + '/campaign/isLiked',
    getCampaignsByUserId: environment.apiUrl + '/campaign/get-campaigns',
    subscribeCampaign: environment.apiUrl + '/campaign/subscribe',
    isSubscribed: environment.apiUrl + '/campaign/is-subscribe',
    getSubscribers: environment.apiUrl + '/campaign/get-subscribers',
    activate: environment.apiUrl + '/campaign/activate',
    deactivate: environment.apiUrl + '/campaign/deactivate',
    approve: environment.apiUrl + '/campaign/verify-subscriber',
    reject: environment.apiUrl + '/campaign/reject-subscriber',
  };

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private http: HttpClient,
    private messageService: MessageService
  ) {}

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

  addCampaign(campaign: CreateCampaign): Observable<Campaign> {
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

  like(campaignId: number, userId: string): Observable<any> {
    return this.http
      .post(this.campaignUrl.like, { campaignId, userId }, this.httpOptions)
      .pipe(
        tap((_) => this.log(`liked campaign id=${campaignId}`)),
        catchError(this.handleError<any>('like'))
      );
  }

  unlike(campaignId: number, userId: string): Observable<any> {
    return this.http
      .post(this.campaignUrl.unlike, { campaignId, userId }, this.httpOptions)
      .pipe(
        tap((_) => this.log(`unliked campaign id=${campaignId}`)),
        catchError(this.handleError<any>('like'))
      );
  }

  isLiked(campaignId: number, userId: string): Observable<boolean> {
    return this.http
      .get<boolean>(
        `${this.campaignUrl.isLiked}/${userId}/${campaignId}`,
        this.httpOptions
      )
      .pipe(
        tap((_) => this.log(`liked campaign id=${campaignId}`)),
        catchError(this.handleError<any>('isLiked'))
      );
  }

  comment(comment: CommentModel): Observable<any> {
    return this.http
      .post(this.campaignUrl.comment, comment, this.httpOptions)
      .pipe(
        tap((_) => this.log(`commented campaign id=${comment.campaignId}`)),
        catchError(this.handleError<any>('comment'))
      );
  }

  getCampaignByUserId(userId: string): Observable<Campaign[]> {
    return this.http
      .get<Campaign[]>(`${this.campaignUrl.getCampaignsByUserId}/${userId}`)
      .pipe(
        tap((_) => this.log('fetched campaigns')),
        catchError(this.handleError<Campaign[]>('getCampaigns', []))
      );
  }

  subscribeCampaign(campaignId: number, userId: string): Observable<any> {
    return this.http
      .post(
        this.campaignUrl.subscribeCampaign,
        { campaignId, userId },
        this.httpOptions
      )
      .pipe(
        tap((_) => this.log(`liked campaign id=${campaignId}`)),
        catchError(this.handleError<any>('like'))
      );
  }

  isSubscribed(campaignId: number, userId: string): Observable<boolean> {
    return this.http
      .post<boolean>(
        `${this.campaignUrl.isSubscribed}`,
        { campaignId, userId },
        this.httpOptions
      )
      .pipe(
        tap((_) => this.log(`liked campaign id=${campaignId}`)),
        catchError(this.handleError<any>('isLiked'))
      );
  }

  getSubscribers(campaignId: number): Observable<Subscriber[]> {
    return this.http
      .get<Subscriber[]>(`${this.campaignUrl.getSubscribers}/${campaignId}`)
      .pipe(
        tap((_) => this.log(`get subscribers of id=${campaignId}`)),
        catchError(this.handleError<Subscriber[]>('isLiked'))
      );
  }

  activateCampaign(campaignId: number): Observable<any> {
    return this.http
      .post(`${this.campaignUrl.activate}`, campaignId, this.httpOptions)
      .pipe(
        tap((_) => this.log(`activated campaign id=${campaignId}`)),
        catchError(this.handleError<any>('activateCampaign'))
      );
  }

  deactivateCampaign(campaignId: number): Observable<any> {
    return this.http
      .post(`${this.campaignUrl.deactivate}`, campaignId, this.httpOptions)
      .pipe(
        tap((_) => this.log(`deactivated campaign id=${campaignId}`)),
        catchError(this.handleError<any>('deactivateCampaign'))
      );
  }

  approveSubscriber(subscriberId: string, campaginId: number): Observable<any> {
    return this.http
      .post(
        `${this.campaignUrl.approve}`,
        { userId: subscriberId, campaignId: campaginId },
        this.httpOptions
      )
      .pipe(
        tap((_) => this.log(`approved subscriber id=${subscriberId}`)),
        catchError(this.handleError<any>('approveSubscriber'))
      );
  }

  rejectSubscriber(subscriberId: string, campaginId: number): Observable<any> {
    return this.http
      .post(
        `${this.campaignUrl.reject}`,
        { userId: subscriberId, campaignId: campaginId },
        this.httpOptions
      )
      .pipe(
        tap((_) => this.log(`rejected subscriber id=${subscriberId}`)),
        catchError(this.handleError<any>('approveSubscriber'))
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
