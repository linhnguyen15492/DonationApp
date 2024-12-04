import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { LoginModel } from '../models/login';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginModel: LoginModel = {} as LoginModel;

  constructor(private authService: AuthService) { }

  onSubmit() {
    console.log(this.loginModel);
    this.authService.login(this.loginModel)
  }
}
