import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegisterModel } from '../models/register';
import { UserRoles } from '../models/userRoles';
import { catchError } from 'rxjs/operators';
import { tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { Router, RouterModule } from '@angular/router';


@Component({
  selector: 'app-signup',
  imports: [FormsModule, CommonModule, RouterModule],
  standalone: true,
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit {
  registerModel: RegisterModel = {} as RegisterModel;
  confirmPassword: string = '';

  roles: UserRoles[] = [{ code: 'Donor', name: 'Người quyên góp' }, { code: 'Donee', name: 'Người cần hỗ trợ' }, { code: 'CharitableOrganization', name: 'Tổ chức từ thiện' }];


  constructor(private authService: AuthService, private router: Router) { }
  ngOnInit(): void {
    this.registerModel.phoneNumber = localStorage.getItem('phoneNumber') || '';
  }

  onSubmit() {
    console.log(this.registerModel);

    if (this.registerModel.password !== this.confirmPassword) {
      alert('Mật khẩu không khớp');
      return;
    }

    this.authService.register(this.registerModel).subscribe({


      next: (res: boolean) => {
        if (res) {
          setTimeout(() => {
            this.router.navigate(['/register-result'])
          }, 1000);
        }
      },
      error: (error: any) => {
        alert('Đăng ký thất bại');
        console.log(error);
      }
    })
  }
}
