import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../core/service/auth.services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent  implements OnInit {

registerForm!: FormGroup;

constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) { }

ngOnInit(): void {
  if (this.authService.isLoggedIn()) {
      this.router.navigate(['/restaurants']);
}

  this.registerForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', [Validators.required]],
  }, { validator: this.passwordMatchValidator });
  
  
}

passwordMatchValidator(form: FormGroup) {
  const password = form.get('password')?.value;
  const confirmPassword = form.get('confirmPassword')?.value;
  return password === confirmPassword ? null : { mismatch: true };
}

onSubmit() {
  if (this.registerForm.invalid) return;
  const { name, email, password} = this.registerForm.value;

  this.authService.register({ name, email, password}).subscribe({
    
      next:() => {
        this.router.navigate(['/login']);
        alert('Registrazione avvenuta con successo! Effettua il login.');
    },
    error: (err: any) => {
      console.error('Registration failed:', err);
      if (err.error?.errors) {
        console.log('Validation errors:', err.error.errors);
      }
      alert('Errore durante la registrazione');
    }
  });
}}