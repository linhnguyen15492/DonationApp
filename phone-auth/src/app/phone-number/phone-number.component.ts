import { Component, OnInit } from '@angular/core';
import firebase from 'firebase/compat/app';
import 'firebase/auth';
import 'firebase/firestore';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import e from 'express';

var config = {
  apiKey: environment.firebase.apiKey,
  authDomain: environment.firebase.authDomain,
  projectId: environment.firebase.projectId,
  storageBucket: environment.firebase.storageBucket,
  messagingSenderId: environment.firebase.messagingSenderId,
  appId: environment.firebase.appId,
};

@Component({
  selector: 'app-phone-number',
  standalone: false,

  templateUrl: './phone-number.component.html',
  styleUrl: './phone-number.component.css',
})
export class PhoneNumberComponent implements OnInit {
  phoneNumber: any;
  reCapchaVerifier: any;

  constructor(private router: Router) {}

  ngOnInit() {
    firebase.initializeApp(config);
  }

  getOTP() {
    this.reCapchaVerifier = new firebase.auth.RecaptchaVerifier(
      'sign-in-button',
      { size: 'invisible' }
    );

    firebase
      .auth()
      .signInWithPhoneNumber(this.phoneNumber, this.reCapchaVerifier)
      .then((confirmationResult) => {
        localStorage.setItem(
          'verificationId',
          JSON.stringify(confirmationResult.verificationId)
        );
        this.router.navigate(['/code']);
      })
      .catch((error) => {
        alert(error.message);
        setTimeout(() => {
          window.location.reload();
        }, 5000);
      });
  }
}
