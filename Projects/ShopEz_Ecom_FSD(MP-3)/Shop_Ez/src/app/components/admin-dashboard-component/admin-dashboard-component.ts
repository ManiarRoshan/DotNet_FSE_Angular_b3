import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ProductsComponent } from '../products-component/products-component';
import { CartComponent } from '../cart-component/cart-component';
import { OrdersComponent } from '../orders-component/orders-component';
import { AdminComponent } from '../admin-component/admin-component';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, ProductsComponent, CartComponent, OrdersComponent, AdminComponent],
  templateUrl: './admin-dashboard-component.html',
  styleUrls: ['./admin-dashboard-component.css']
})
export class AdminDashboardComponent {
  private auth = inject(AuthService);
  private router = inject(Router);
  activeTab: 'store' | 'admin' = 'store';
  storeView: 'products' | 'cart' | 'orders' = 'products';

  logout() {
    this.auth.logout();
    this.router.navigate(['/admin-login']);
  }
}