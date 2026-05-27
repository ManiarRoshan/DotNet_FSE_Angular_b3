import { Component, inject, Input } from '@angular/core';
import { CommonModule, Location } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-page-back-button',
  standalone: true,
  imports: [CommonModule],
  template: `
    <button type="button" class="page-back-btn" (click)="goBack()" [attr.aria-label]="label">
      <i class="bi bi-arrow-left"></i>
      <span>{{ label }}</span>
    </button>
  `
})
export class PageBackButtonComponent {
  @Input() label = 'Back';
  @Input() fallbackRoute = '/home';
  @Input() onBack?: () => void;

  private location = inject(Location);
  private router = inject(Router);

  goBack(): void {
    if (this.onBack) {
      this.onBack();
      return;
    }
    if (window.history.length > 1) {
      this.location.back();
    } else {
      this.router.navigate([this.fallbackRoute]);
    }
  }
}
