import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.css',
})
export class NotificationComponent {
  private api_url = 'https://localhost:7112/messageHub';

  private hubConnectionBuilder!: HubConnection;

  offers: any[] = [];
  constructor() {}
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
      this.offers.push(result);
    });
  }
}
