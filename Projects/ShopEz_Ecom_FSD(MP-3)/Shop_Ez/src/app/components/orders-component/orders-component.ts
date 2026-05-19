import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { OrderService } from '../../services/order-service';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../services/toast-service';
import { ImageService } from '../../services/image-service';
import { Order } from '../../models/order.model';

@Component({
  selector: 'app-orders-component',
  standalone: true,
  imports: [CommonModule, RouterLink],
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

  ngOnInit() {
    if (!this.auth.isLoggedIn()) return;
    this.loadOrders();
  }

  loadOrders() {
    this.loading = true;
    this.orderService.getMyOrders().subscribe({
      next: (orders) => {
        this.orders = orders;
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

  getOrderStatus(order: Order): string {
    const orderDate = new Date(order.orderDate);
    const daysDiff = Math.floor((Date.now() - orderDate.getTime()) / (1000 * 60 * 60 * 24));
    return daysDiff > 7 ? 'Delivered' : 'Processing';
  }

  getOrderStatusClass(order: Order): string {
    const status = this.getOrderStatus(order);
    return status === 'Delivered' ? 'status-delivered' : 'status-processing';
  }
}