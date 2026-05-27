import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { OrderService } from '../../services/order-service';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../services/toast-service';
import { ImageService } from '../../services/image-service';
import { Order } from '../../models/order.model';
import { PageBackButtonComponent } from '../page-back-button/page-back-button';

@Component({
  selector: 'app-orders-component',
  standalone: true,
  imports: [CommonModule, RouterLink, PageBackButtonComponent],
  templateUrl: './orders-component.html',
  styleUrls: ['./orders-component.css']
})
export class OrdersComponent implements OnInit {
  private orderService = inject(OrderService);
  private auth = inject(AuthService);
  private toast = inject(ToastService);
  private cdr = inject(ChangeDetectorRef);
  private imageService = inject(ImageService);

  orders: Order[] = [];
  loading = false;
  cancellingId: number | null = null;

  ngOnInit() {
    if (!this.auth.isLoggedIn()) return;
    this.loadOrders();
  }

  loadOrders() {
    this.loading = true;
    this.orderService.getMyOrders().subscribe({
      next: (orders) => {
        this.orders = orders.sort((a, b) =>
          this.orderDateValue(b).getTime() - this.orderDateValue(a).getTime()
        );
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.loading = false;
        this.toast.show('Could not load orders', 'error');
        console.error(err);
        this.cdr.detectChanges();
      }
    });
  }

  getImageUrl(imageUrl: string): string {
    return this.imageService.getFullImageUrl(imageUrl);
  }

  isCancelled(order: Order): boolean {
    return order.orderStatus === 'Cancelled';
  }

  getDisplayStatus(order: Order): string {
    if (this.isCancelled(order)) return 'Cancelled';
    const orderDate = this.orderDateValue(order);
    const daysDiff = Math.floor((Date.now() - orderDate.getTime()) / (1000 * 60 * 60 * 24));
    return daysDiff > 7 ? 'Delivered' : 'Processing';
  }

  canCancel(order: Order): boolean {
    return !this.isCancelled(order) && this.getDisplayStatus(order) === 'Processing';
  }

  cancelOrder(order: Order) {
    if (!confirm(`Cancel order #${this.formatOrderDisplayId(order.orderId)}?`)) return;
    this.cancellingId = order.orderId;
    this.orderService.cancelOrder(order.orderId).subscribe({
      next: () => {
        this.toast.show('Order cancelled successfully', 'success');
        this.cancellingId = null;
        this.loadOrders();
      },
      error: (err) => {
        this.cancellingId = null;
        this.toast.show(err.error?.message || err.error || 'Could not cancel order', 'error');
        this.cdr.detectChanges();
      }
    });
  }

  orderDateValue(order: Order): Date {
    const raw = order.orderDate;
    if (!raw) return new Date();
    const t = String(raw).trim();
    if (/Z|[+-]\d{2}:?\d{2}$/.test(t)) return new Date(t);
    if (t.includes('T')) return new Date(t + 'Z');
    return new Date(t);
  }

  formatOrderDisplayId(orderId: number): string {
    return String(Math.floor(orderId)).padStart(5, '0');
  }
}
