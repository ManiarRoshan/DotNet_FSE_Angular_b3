import { Component, inject, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../services/toast-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './toast-component.html',
  styleUrls: ['./toast-component.css']
})
export class ToastComponent implements OnDestroy {
  private toastService = inject(ToastService);
  private subscription: Subscription;
  private timeoutId: any;

  message: string | null = null;
  type: 'success' | 'error' | 'info' = 'success';

  constructor() {
    this.subscription = this.toastService.toast$.subscribe(toast => {
      if (toast) {
        this.message = toast.message;
        this.type = toast.type;
        if (this.timeoutId) clearTimeout(this.timeoutId);
        this.timeoutId = setTimeout(() => this.clear(), 3000);
      } else {
        this.message = null;
      }
    });
  }

  clear() {
    this.message = null;
    this.toastService.clear();
    if (this.timeoutId) clearTimeout(this.timeoutId);
  }

  closeToast() {
    this.clear();
  }

  ngOnDestroy() {
    if (this.subscription) this.subscription.unsubscribe();
    if (this.timeoutId) clearTimeout(this.timeoutId);
  }
}