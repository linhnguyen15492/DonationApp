import { Component, Input, OnInit } from '@angular/core';
import { User } from '../core/models/user';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
})
export class SignupComponent {
  user: User = { username: 'lĩnh', email: 'abc@gmail.com', password: '123456' };

  constructor(private authService: AuthService) {}

  onSubmit() {
    console.log(this.user);

    this.authService.signUp(this.user);
  }
}
