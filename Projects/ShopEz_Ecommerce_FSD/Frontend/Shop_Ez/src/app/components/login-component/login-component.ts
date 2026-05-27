import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../services/toast-service';

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
  private toast = inject(ToastService);
  form = this.fb.group({ email: ['', [Validators.required, Validators.email]], password: ['', Validators.required] });
  loading = false;
  showPassword = false;
  emailLocked = true;
  passwordLocked = true;

  unlockEmail(): void {
    this.emailLocked = false;
  }

  unlockPassword(): void {
    this.passwordLocked = false;
  }

  submit() {
    if (this.form.invalid) return;
    this.loading = true;

    const email = this.form.value.email ?? '';
    const password = this.form.value.password ?? '';

    this.auth.login({ email, password }).subscribe({
      next: () => {
        this.loading = false;
        this.toast.show('Login successful! Welcome back.', 'success');
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Login error', err);
        this.loading = false;
        const msg = err.error?.message || err.error || err.message || 'Invalid credentials';
        this.toast.show('Login failed: ' + msg, 'error');
      }
    });
  }
}
