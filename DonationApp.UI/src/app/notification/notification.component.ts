import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-notification',
  imports: [CommonModule],
  standalone: true,
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css',
})
export class NotificationComponent {
  private api_url = 'https://localhost:7112/messageHub';

  private hubConnectionBuilder!: HubConnection;

  private notificationsSubject = new BehaviorSubject<any[]>([]);
  notifications$ = this.notificationsSubject.asObservable();

  offers: any[] = [];
  constructor(private httpClient: HttpClient) {}
  ngOnInit(): void {
    this.hubConnectionBuilder = new HubConnectionBuilder()
      .withUrl(this.api_url)
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnectionBuilder
      .start()
      .then(() => console.log('Connection started.......!'))
      .catch((err) => console.log('Error while connect with server'));

    this.hubConnectionBuilder.on('PushNotificationAsync', (result: any) => {
      console.log(result);
      this.offers.unshift(result);
    });

    this.getWeather();
  }

  getWeather(): void {
    this.httpClient.get('https://localhost:7112/api/Weather').subscribe({
      next: (data: any) => {
        // data.forEach((element: any) => {
        //   this.offers.push(element);
        // });
        console.log(data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
