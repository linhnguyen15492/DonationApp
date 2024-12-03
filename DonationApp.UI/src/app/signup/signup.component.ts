import { Component, Input, OnInit } from '@angular/core';
import { User } from '../core/models/user';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit {
  @Input() user: User = {} as User;

  name: string = 'aaaa';

  constructor(private authService: AuthService) { }
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

  onSubmit() {
    console.log(this.user);

    this.authService.signUp(this.user);
  }
}
