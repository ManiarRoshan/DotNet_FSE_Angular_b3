import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

export interface ToastMessage {
  message: string;
  type: 'success' | 'error' | 'info';
}

@Injectable({ providedIn: 'root' })
export class ToastService {
  private toastSubject = new Subject<ToastMessage | null>();
  toast$ = this.toastSubject.asObservable();

  show(message: string, type: 'success' | 'error' | 'info' = 'success') {
    // Emit new toast
    this.toastSubject.next({ message, type });
    // Auto-clear after 3 seconds
    setTimeout(() => this.clear(), 3000);
  }

  clear() {
    this.toastSubject.next(null);
  }
}