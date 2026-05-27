import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from '../products-component/products-component';
import { AdminComponent } from '../admin-component/admin-component';
import { PageBackButtonComponent } from '../page-back-button/page-back-button';
import { ImageService } from '../../services/image-service';
import { Product } from '../../models/product.model';
import { Order } from '../../models/order.model';
import { AdminDashboardService } from '../../services/admin-dashboard.service';
import { getActiveProducts } from '../../utils/product.utils';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, ProductsComponent, AdminComponent, PageBackButtonComponent],
  templateUrl: './admin-dashboard-component.html',
  styleUrls: ['./admin-dashboard-component.css']
})
export class AdminDashboardComponent implements OnInit, OnDestroy {
  activeView: 'store' | 'admin' | null = null;

  private adminHub = inject(AdminDashboardService);
  private imageService = inject(ImageService);
  private hubSub?: Subscription;

  inventoryPreview: Product[] = [];
  totalProducts = 0;
  inStockCount = 0;
  lowStockCount = 0;
  orderCount = 0;
  statsLoading = true;

  ngOnInit(): void {
    const cached = this.adminHub.snapshot;
    if (cached) {
      this.applyHubData(cached.products, cached.orders);
    } else {
      this.statsLoading = true;
      this.adminHub.preload(true);
    }

    this.hubSub = this.adminHub.hubData$.subscribe(data => {
      if (data) {
        this.applyHubData(data.products, data.orders);
      }
    });
  }

  ngOnDestroy(): void {
    this.hubSub?.unsubscribe();
  }

  openView(view: 'store' | 'admin'): void {
    this.activeView = view;
  }

  closeView = (): void => {
    this.activeView = null;
  };

  private applyHubData(products: Product[], orders: Order[]): void {
    const active = getActiveProducts(products);
    this.totalProducts = active.length;
    this.inStockCount = active.filter(p => p.stock > 5).length;
    this.lowStockCount = active.filter(p => p.stock > 0 && p.stock <= 5).length;
    this.orderCount = orders.length;
    this.inventoryPreview = active.slice(0, 8);
    this.statsLoading = false;
  }

  getImageUrl(product: Product): string {
    return this.imageService.getFullImageUrl(product.imageUrl);
  }
}
