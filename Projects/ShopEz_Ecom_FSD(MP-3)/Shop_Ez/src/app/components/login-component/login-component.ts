import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login-component',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './login-component.html',
  styleUrls: ['./login-component.css']
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);
  form = this.fb.group({ email: ['', [Validators.required, Validators.email]], password: ['', Validators.required] });
  loading = false;
  
  submit() { 
    if (this.form.invalid) return; 
    this.loading = true; 
    
    const email = this.form.value.email ?? '';
    const password = this.form.value.password ?? '';
    
 this.auth.login({ email, password }).subscribe({
  next: () => this.router.navigate(['/home']),
  error: (err) => {
    console.error('Login error', err);
    this.loading = false;
    alert('Login failed: ' + (err.error || err.message));
  }
});
  }
}
