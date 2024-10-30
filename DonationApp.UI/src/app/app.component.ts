import { Component } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'SignalRClient';

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
