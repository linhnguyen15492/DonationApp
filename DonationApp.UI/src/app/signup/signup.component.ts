import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RegisterModel } from '../models/register';
import { UserRoles } from '../models/userRoles';


@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css',
})
export class SignupComponent implements OnInit {
  registerModel: RegisterModel = {} as RegisterModel;

  roles: UserRoles[] = [{ code: 'Donor', name: 'Người quyên góp' }, { code: 'Donee', name: 'Người cần hỗ trợ' }, { code: 'CharitableOrganization', name: 'Tổ chức từ thiện' }];


  constructor(private authService: AuthService) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  onSubmit() {
    console.log(this.registerModel);

    this.authService.register(this.registerModel);
  }
}
