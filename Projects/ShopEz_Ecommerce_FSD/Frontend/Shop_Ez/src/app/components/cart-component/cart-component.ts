import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartItem } from '../../models/cart.model';
import { CartService } from '../../services/cart-service';
import { ToastService } from '../../services/toast-service';
import { ImageService } from '../../services/image-service';
import { PageBackButtonComponent } from '../page-back-button/page-back-button';

@Component({
  selector: 'app-cart-component',
  standalone: true,
  imports: [CommonModule, RouterLink, PageBackButtonComponent],
  templateUrl: './cart-component.html',
  styleUrls: ['./cart-component.css']
})
export class CartComponent implements OnInit {
  private cart = inject(CartService);
  private router = inject(Router);
  private toast = inject(ToastService);
  private imageService = inject(ImageService);

  items: CartItem[] = [];
  subtotal = 0;
  shipping = 0;
  total = 0;

  ngOnInit() {
    this.load();
  }

  load() {
    this.items = this.cart.getCart();
    this.subtotal = this.cart.getCartTotal();
    this.shipping = this.subtotal > 999 ? 0 : 99;
    this.total = this.subtotal + this.shipping;
  }

  getImageUrl(item: CartItem): string {
    return this.imageService.getFullImageUrl(item.product.imageUrl);
  }

  updateQty(id: number, newQty: number) {
    const item = this.items.find(i => i.product.productId === id);
    if (!item) return;

    if (newQty < 1) return;

    if (newQty > item.product.stock) {
      this.toast.show(`Only ${item.product.stock} in stock`, 'error');
      return;
    }

    this.cart.updateQuantity(id, newQty);
    this.load();
  }

  remove(id: number) {
    this.cart.removeFromCart(id);
    this.load();
    this.toast.show('Item removed from cart', 'info');
  }

  goCheckout() {
    // Final stock validation before checkout
    for (const item of this.items) {
      if (item.quantity > item.product.stock) {
        this.toast.show(`${item.product.name} quantity exceeds available stock (${item.product.stock})`, 'error');
        return;
      }
    }
    this.router.navigate(['/checkout']);
  }
}