import { Component, Input, OnInit } from '@angular/core';
import firebase from 'firebase/compat/app';
import 'firebase/auth';
import 'firebase/firestore';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { environment } from 'src/environments/environment.development';

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
  standalone: true,
  imports: [FormsModule],
  templateUrl: './phone-number.component.html',
  styleUrl: './phone-number.component.css',
})
export class PhoneNumberComponent implements OnInit {
  phoneNumber: any;
  reCapchaVerifier!: firebase.auth.RecaptchaVerifier; // Update type and add definite assignment;

  constructor(private router: Router) {
    // Initialize Firebase in constructor instead of ngOnInit
    if (!firebase.apps.length) {
      firebase.initializeApp(config);
    }
  }

  ngOnInit() {
    // Remove Firebase initialization from here
  }

  getOTP() {
    // test
    // localStorage.setItem('phoneNumber', this.phoneNumber)
    // this.router.navigate(['/code'])

    // Create new reCAPTCHA verifier instance
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
        localStorage.setItem('phoneNumber', this.phoneNumber);
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
