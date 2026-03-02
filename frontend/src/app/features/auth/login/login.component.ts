import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators, } from '@angular/forms';
import { AuthService } from '../../../core/service/auth.services';
import { LoginRequest } from '../../../models/login.model';
import { Router } from '@angular/router';
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


constructor(private authService: AuthService, private router: Router) { }

onSubmit() {
  if (this.loginForm.invalid) return; // controllo di sicurezza per non inviare dati vuoti o sbagliati.

  this.authService.login(this.loginForm.value as LoginRequest) 
  
  .subscribe({
    next: (response) => {
      console.log('RESPONSE COMPLETA:', response); // ← AGGIUNGI QUESTO
    console.log('Token:', response.data.token);
    console.log('User:', response.data.user);
      setTimeout(() => {
          this.router.navigate(['/restaurants']) // redirect immediato
    },0);
      console.log('Login successful:', response);
    },
    error: (error) => {
      console.error('Login failed:', error);
    }
  });}
}