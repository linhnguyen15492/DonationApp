import { Component, OnInit } from '@angular/core';
import firebase from 'firebase/compat/app';
import "firebase/auth";
import "firebase/firestore";
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgOtpInputModule } from 'ng-otp-input';

@Component({
  selector: 'app-code',
  standalone: true,
  imports: [
    NgOtpInputModule,
    FormsModule
  ],
  templateUrl: './code.component.html',
  styleUrl: './code.component.css'
})
export class CodeComponent implements OnInit {
  otp!: string
  verify: any;
  phoneNumber: any;
  constructor(private router: Router) { }

  config = {
    allowNumbersOnly: true,
    length: 6,
    isPasswordInput: false,
    disableAutoFocus: false,
    placeholder: '',
    inputStyles: {
      width: '50px',
      height: '50px',
    },
  };

  ngOnInit() {
    this.verify = JSON.parse(localStorage.getItem('verificationId') || '{}')
    this.phoneNumber = localStorage.getItem('phoneNumber')
    console.log(this.verify);
  }

  onOtpChange(otpCode: any) {
    this.otp = otpCode
  }

  handleClick() {

    // test 
    // this.router.navigate(['/signup'])

    var credentials = firebase.auth.PhoneAuthProvider.credential(this.verify, this.otp);
    firebase.auth().signInWithCredential(credentials).then((response) => {
      console.log(response);
      localStorage.setItem('user_data', JSON.stringify(response));
      this.router.navigate(['/signup'])
    }).catch((error) => {
      localStorage.removeItem('phoneNumber')
      localStorage.removeItem('verificationId')
      alert(error.message)
    })
  }
}
