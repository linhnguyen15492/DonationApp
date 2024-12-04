import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { LoginModel } from '../models/login';
import { RegisterModel } from '../models/register';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = environment.apiUrl;
  private tokenKey = 'authToken';

  constructor(private http: HttpClient, private router: Router) { }

  register(registerModel: RegisterModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/Account/register`, registerModel);
  }

  login(loginModel: LoginModel) {
    this.http.post(`${this.apiUrl}/Account/login`, loginModel).subscribe({
      next: (response: any) => {
        const token = response.accessToken; // Lấy token từ API
        sessionStorage.setItem('accessToken', token); // Lưu token vào LocalStorage
        console.log('Login successful, token saved');
        this.router.navigate(['/']); // Điều hướng sau khi đăng nhập
      },
      error: (err) => {
        console.error('Login failed:', err);
      }
    });
  };

  // Lưu token
  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  // Lấy token
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  // Xóa token
  removeToken() {
    localStorage.removeItem(this.tokenKey);
  }

  // Kiểm tra trạng thái đăng nhập
  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}


