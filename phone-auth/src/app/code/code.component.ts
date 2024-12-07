import { Component, OnInit } from '@angular/core';
import firebase from 'firebase/compat/app';
import "firebase/auth";
import "firebase/firestore";
import { Router } from '@angular/router';

@Component({
  selector: 'app-code',
  standalone: false,
  
  templateUrl: './code.component.html',
  styleUrl: './code.component.css'
})
export class CodeComponent implements OnInit {
  otp!: string
  verify: any;
  constructor(private router: Router){}

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

  ngOnInit(){
    this.verify = JSON.parse(localStorage.getItem('verificationId') ||'{}')
    console.log(this.verify);
  }
  
  onOtpChange(otpCode:any) {
    this.otp = otpCode
  }

  handleClick(){
    var credentials = firebase.auth.PhoneAuthProvider.credential(this.verify,this.otp);
    firebase.auth().signInWithCredential(credentials).then((response)=>{
      console.log(response);
      localStorage.setItem('user_data',JSON.stringify(response));
      this.router.navigate(['/dashboard'])
    }).catch((error)=>{
      alert(error.message)
    })
  }
}
