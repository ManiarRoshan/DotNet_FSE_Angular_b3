import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastService } from '../../services/toast-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './toast-component.html',
  styleUrls: ['./toast-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ToastComponent implements OnDestroy {
  private toastService = inject(ToastService);
  private cdr = inject(ChangeDetectorRef);
  private subscription: Subscription;

  message: string | null = null;
  type: 'success' | 'error' | 'info' = 'success';

  constructor() {
    this.subscription = this.toastService.toast$.subscribe(toast => {
      if (toast) {
        this.message = toast.message;
        this.type = toast.type;
      } else {
        this.message = null;
      }
      this.cdr.markForCheck();
    });
  }

  clear() {
    this.toastService.clear();
  }

  closeToast() {
    this.clear();
  }

  ngOnDestroy() {
    if (this.subscription) this.subscription.unsubscribe();
  }
}