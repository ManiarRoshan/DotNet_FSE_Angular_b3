import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { CartItem } from '../models/cart.model';
import { Product } from '../models/product.model';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class CartService implements OnDestroy {
  private cartSubject = new BehaviorSubject<CartItem[]>([]);
  cart$ = this.cartSubject.asObservable();
  private authSub: Subscription;
  private currentUserId: number | null = null;

  constructor(private auth: AuthService) {
    // Listen to login/logout changes
    this.authSub = this.auth.authState$.subscribe(() => {
      this.loadCartForCurrentUser();
    });
    this.loadCartForCurrentUser();
  }

  ngOnDestroy() {
    this.authSub?.unsubscribe();
  }

  private getCartKey(userId: number | null = null): string {
    const uid = userId ?? this.auth.getUserId();
    if (uid) {
      return `shopCart_${uid}`;
    }
    return 'shopCart_guest';
  }

  private loadCartForCurrentUser(): void {
    const userId = this.auth.getUserId();
    if (userId !== this.currentUserId) {
      this.currentUserId = userId;
      const cart = this.getCart();
      this.cartSubject.next(cart);
    }
  }

  getCart(): CartItem[] {
    const key = this.getCartKey();
    const cartStr = localStorage.getItem(key);
    return cartStr ? JSON.parse(cartStr) : [];
  }

  private saveCart(cart: CartItem[]): void {
    const key = this.getCartKey();
    localStorage.setItem(key, JSON.stringify(cart));
    this.cartSubject.next(cart);
  }

  addToCart(product: Product, quantity: number = 1): void {
    const cart = this.getCart();
    const existing = cart.find(item => item.product.productId === product.productId);
    const newQuantity = (existing ? existing.quantity : 0) + quantity;

    if (newQuantity > product.stock) {
      console.warn(`Cannot add more than ${product.stock} of ${product.name}`);
      return;
    }

    if (existing) existing.quantity = newQuantity;
    else cart.push({ product, quantity });

    this.saveCart(cart);
  }

  removeFromCart(productId: number): void {
    let cart = this.getCart();
    cart = cart.filter(item => item.product.productId !== productId);
    this.saveCart(cart);
  }

  updateQuantity(productId: number, quantity: number): void {
    const cart = this.getCart();
    const item = cart.find(i => i.product.productId === productId);
    if (item) {
      if (quantity <= 0) this.removeFromCart(productId);
      else {
        item.quantity = quantity;
        this.saveCart(cart);
      }
    }
  }

  clearCart(): void {
    const key = this.getCartKey();
    localStorage.removeItem(key);
    this.cartSubject.next([]);
  }

  getCartTotal(): number {
    return this.getCart().reduce((t, i) => t + i.product.price * i.quantity, 0);
  }

  getCartCount(): number {
    return this.getCart().reduce((c, i) => c + i.quantity, 0);
  }

  // Force reload (useful after login)
  reloadCart(): void {
    this.loadCartForCurrentUser();
  }
}