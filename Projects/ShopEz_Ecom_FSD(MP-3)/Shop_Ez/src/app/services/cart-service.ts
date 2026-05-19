import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem } from '../models/cart.model';
import { Product } from '../models/product.model';

@Injectable({ providedIn: 'root' })
export class CartService {
  private cartKey = 'shopCart';
  private cartSubject = new BehaviorSubject<CartItem[]>(this.getCart());
  cart$ = this.cartSubject.asObservable();

  getCart(): CartItem[] {
    const cartStr = localStorage.getItem(this.cartKey);
    return cartStr ? JSON.parse(cartStr) : [];
  }

  private saveCart(cart: CartItem[]): void {
    localStorage.setItem(this.cartKey, JSON.stringify(cart));
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
    localStorage.removeItem(this.cartKey);
    this.cartSubject.next([]);
  }

  getCartTotal(): number {
    return this.getCart().reduce((t, i) => t + i.product.price * i.quantity, 0);
  }

  getCartCount(): number {
    return this.getCart().reduce((c, i) => c + i.quantity, 0);
  }
}