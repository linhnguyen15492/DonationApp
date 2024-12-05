import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { LoginModel } from '../models/login';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { ErrorMessageComponent } from '../error-message/error-message.component';

@Component({
    selector: 'app-login',
    imports: [FormsModule, ErrorMessageComponent],
    templateUrl: './login.component.html',
    styleUrl: './login.component.css'
})
export class LoginComponent {
  errorMessage: string = '';
  loginModel: LoginModel = {} as LoginModel;
  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.loginModel).subscribe({
      next: (response: User) => {
        this.router.navigate(['/']); // Điều hướng sau khi đăng nhập
      },
      error: (error: any) => {
        this.errorMessage = 'Tên đăng nhập hoặc mật khẩu không đúng';
      },
    });
  }
}
