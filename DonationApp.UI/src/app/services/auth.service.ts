import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
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
  private userKey = 'currentUser';

  constructor(private http: HttpClient) {}

  register(registerModel: RegisterModel): Observable<any> {
    return this.http
      .post(`${this.apiUrl}/Account/register`, registerModel)
      .pipe(
        map((response: any) => {
          console.log(response);
          return response.isSuccess;
        })
      );
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
          accountNumber: response.accountNumber,
          fullName: response.fullName,
          roles: response.roles,
          balance: response.balance,
        };
        console.log(user);
        this.setToken(token);
        this.setUser(user);
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
    return localStorage.getItem(this.tokenKey);
  }

  setUser(user: User) {
    console.log(user);
    sessionStorage.setItem(this.userKey, JSON.stringify(user));
  }

  getUser(): User | null {
    const json = localStorage.getItem(this.userKey);

    let user: User;
    user = JSON.parse(json!);

    console.log('parse', user);

    return user;
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
