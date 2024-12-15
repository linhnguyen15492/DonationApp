import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterModel } from '../models/register';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-charity-documents',
  imports: [],
  templateUrl: './charity-documents.component.html',
  styleUrl: './charity-documents.component.css',
})
export class CharityDocumentsComponent {
  registerModel: RegisterModel = {} as RegisterModel;

  constructor(private router: Router, private authService: AuthService) {
    let navigation = this.router.getCurrentNavigation();

    if (navigation && navigation.extras.state) {
      let model = navigation.extras.state['model'];
      console.log('model', model);
      this.registerModel = model.registerModel;
    }
  }

  ngOnInit(): void {}

  onSubmit() {
    this.authService.register(this.registerModel).subscribe({
      next: (res: boolean) => {
        if (res) {
          setTimeout(() => {
            this.router.navigate(['/register-result']);
          }, 1000);
        }
      },
      error: (error: any) => {
        alert('Đăng ký thất bại');
        console.log(error);
      },
    });
  }
}
