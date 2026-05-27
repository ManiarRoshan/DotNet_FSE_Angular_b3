import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../services/toast-service';

@Component({
  selector: 'app-register-component',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './register-component.html',
  styleUrls: ['./register-component.css']
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);
  private toast = inject(ToastService);  

  form = this.fb.group({ 
    name: ['', [Validators.required, Validators.minLength(2)]], 
    email: ['', [Validators.required, Validators.email]], 
    password: ['', [Validators.required, Validators.minLength(6)]], 
    confirm: ['', Validators.required] 
  }, { validators: this.pwdMatch });
  
  loading = false;
  errorMessage = '';
  showPassword = false;
  showConfirmPassword = false;
  
  pwdMatch(group: AbstractControl): ValidationErrors | null { 
    return group.get('password')?.value === group.get('confirm')?.value ? null : { mismatch: true }; 
  }
  
  submit() { 
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    
    const name = this.form.value.name ?? '';
    const email = this.form.value.email ?? '';
    const password = this.form.value.password ?? '';
    
    this.loading = true;
    this.errorMessage = '';
    
    this.auth.register({ name, email, password, role: 'Customer' }).subscribe({
      next: () => {
        this.toast.show('Registration successful! Please login.', 'success');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.loading = false;
        let msg = 'Registration failed. ';
        if (err.status === 400) msg = 'Email already exists or invalid data.';
        else if (err.status === 0) msg = 'Cannot connect to server.';
        this.toast.show(msg, 'error');
      }
    });
  }
}