import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { LoginModel } from '../models/login';
import { RegisterModel } from '../models/register';
import { User } from '../models/user';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser: Observable<User | null> =
    this.currentUserSubject.asObservable();

  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

  private apiUrl = environment.apiUrl;
  private tokenKey = 'authToken';

  constructor(private http: HttpClient) {}

  register(registerModel: RegisterModel): Observable<any> {
    return this.http.post(`${this.apiUrl}/Account/register`, registerModel);
  }

  login(loginModel: LoginModel): Observable<User> {
    return this.http.post(`${this.apiUrl}/Account/login`, loginModel).pipe(
      map((response: any) => {
        const token = response.accessToken;
        const userId = response.userId;
        const user: User = {
          id: userId,
          username: loginModel.username,
          token: token,
        };
        console.log(user);
        this.setToken(token);
        this.currentUserSubject.next(user);
        this.isLoggedInSubject.next(true);
        return user;
      })
    );
  }

  // Lưu token
  setToken(token: string) {
    sessionStorage.setItem(this.tokenKey, token);
  }

  // Lấy token
  getToken(): string | null {
    return sessionStorage.getItem(this.tokenKey);
  }

  // Xóa token
  removeToken() {
    sessionStorage.removeItem(this.tokenKey);
  }

  // Kiểm tra trạng thái đăng nhập
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  logout() {
    // Xóa token khỏi sessionStorage
    sessionStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.isLoggedInSubject.next(false);
  }

  checkToken() {
    // Kiểm tra xem token có tồn tại và hợp lệ không
    const token = sessionStorage.getItem('token');
    if (token) {
      this.http.get<User>('/api/user').subscribe(
        (user) => {
          this.currentUserSubject.next(user);
          this.isLoggedInSubject.next(true);
        },
        (error) => {
          this.logout();
        }
      );
    }
  }
}
