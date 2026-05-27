import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-success',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './order-success-component.html',
  styleUrls: ['./order-success-component.css']
})
export class OrderSuccessComponent implements OnInit {
  orderData = { orderId: 0, totalAmount: 0 };

  formatOrderId(id: number): string {
    return String(Math.floor(id)).padStart(5, '0');
  }

  ngOnInit() {
    const stored = localStorage.getItem('lastOrder');
    if (stored) {
      this.orderData = JSON.parse(stored);
      localStorage.removeItem('lastOrder');
    } else {
      // fallback (should not happen)
      this.orderData = { orderId: Math.floor(Math.random() * 10000), totalAmount: 0 };
    }
  }
}