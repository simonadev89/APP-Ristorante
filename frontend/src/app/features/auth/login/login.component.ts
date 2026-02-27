import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.services';
import { LoginRequest } from '../login.model';
@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {



loginForm = new FormGroup({
  email: new FormControl('', [Validators.required, Validators.email]),
  password: new FormControl('', [Validators.required, Validators.minLength(6)])
});


constructor(private authService: AuthService) { }

onSubmit() {
  if (this.loginForm.invalid) return; // controllo di sicurezza per non inviare dati vuoti o sbagliati.

  this.authService.login(this.loginForm.value as LoginRequest) 
  // LoginRequest convince TypeScript che i dati del form corrispondono all'interfaccia LoginRequest, 
  // garantendo così che email e password siano presenti e correttamente formattati.
  .subscribe({
    next: (response) => {
      console.log('Login successful:', response);
    },
    error: (error) => {
      console.error('Login failed:', error);
    }
  });}
}