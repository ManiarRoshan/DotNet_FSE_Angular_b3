import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';  

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './admin-login-component.html',
  styleUrls: ['./admin-login-component.css']
})
export class AdminLoginComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);

  form = this.fb.group({
    email: ['admin@shopez.com', [Validators.required, Validators.email]],
    password: ['admin123', Validators.required]
  });

  loading = false;

  // The form pre-fills for demo convenience, but actual login goes to backend
submit() {
  if (this.form.invalid) return;
  this.loading = true;
  const email = this.form.value.email ?? '';
  const password = this.form.value.password ?? '';
  this.auth.login({ email, password }).subscribe({
    next: () => {
      if (this.auth.isAdmin()) {
        this.router.navigate(['/admin-dashboard']);
      } else {
        alert('You are not an admin. Please use admin credentials.');
        this.auth.logout();
        this.loading = false;
      }
    },
    error: (err) => {
      this.loading = false;
      alert('Login failed: ' + (err.error || 'Invalid credentials'));
    }
  });
}
}
