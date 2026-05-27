import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, forkJoin } from 'rxjs';
import { Product } from '../models/product.model';
import { Order } from '../models/order.model';
import { ProductService } from './product-service';
import { OrderService } from './order-service';

export interface AdminHubData {
  products: Product[];
  orders: Order[];
}

@Injectable({ providedIn: 'root' })
export class AdminDashboardService {
  private productService = inject(ProductService);
  private orderService = inject(OrderService);

  private readonly dataSubject = new BehaviorSubject<AdminHubData | null>(null);
  readonly hubData$ = this.dataSubject.asObservable();

  private fetchInFlight = false;

  get snapshot(): AdminHubData | null {
    return this.dataSubject.value;
  }

  get isLoading(): boolean {
    return this.fetchInFlight;
  }


  preload(force = false): void {
    if (this.fetchInFlight) return;
    if (!force && this.dataSubject.value) return;

    this.fetchInFlight = true;
    forkJoin({
      products: this.productService.getProductsForAdmin(),
      orders: this.orderService.getAllOrders()
    }).subscribe({
      next: ({ products, orders }) => {
        this.dataSubject.next({ products, orders });
        this.fetchInFlight = false;
      },
      error: () => {
        this.fetchInFlight = false;
      }
    });
  }

  invalidate(): void {
    this.dataSubject.next(null);
  }

  updateCache(products: Product[], orders?: Order[]): void {
    const current = this.dataSubject.value;
    this.dataSubject.next({
      products,
      orders: orders ?? current?.orders ?? []
    });
  }
}
