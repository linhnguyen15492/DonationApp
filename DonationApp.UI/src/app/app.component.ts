import { Component } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from '@microsoft/signalr';
import { AuthService } from './services/auth.service';
import { User } from './models/user';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: false
})
export class AppComponent {
  constructor(private authService: AuthService) {}

  isLoggedIn: boolean = false;
  user: User | null = null;

  ngOnInit() {
    this.authService.currentUser.subscribe((user) => {
      this.isLoggedIn = user !== null;
      this.user = user;
    });
  }

  logout() {
    this.authService.logout();
  }
}
