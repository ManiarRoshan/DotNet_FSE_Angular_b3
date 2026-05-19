import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { OrderDTO } from '../../models/order.model';
import { CartService } from '../../services/cart-service';
import { OrderService } from '../../services/order-service';
import { AuthService } from '../../services/auth.service';
import { ToastService } from '../../services/toast-service';

@Component({
  selector: 'app-checkout-component',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './checkout-component.html',
  styleUrls: ['./checkout-component.css']
})
export class CheckoutComponent implements OnInit {
  private fb = inject(FormBuilder);
  private cart = inject(CartService);
  private order = inject(OrderService);
  private auth = inject(AuthService);
  private router = inject(Router);
  private toast = inject(ToastService);

  form = this.fb.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', Validators.required],
    address: ['', Validators.required],
    city: ['', Validators.required],
    pincode: ['', Validators.required],
    paymentMethod: ['cod', Validators.required]
  });

  cartItems = this.cart.getCart();
  subtotal = this.cart.getCartTotal();
  shipping = this.subtotal > 999 ? 0 : 99;
  total = this.subtotal + this.shipping;
  loading = false;

  ngOnInit() {
    if (this.cartItems.length === 0) {
      this.router.navigate(['/cart']);
      return;
    }
    const user = this.auth.getCurrentUser();
    if (user) {
      this.form.patchValue({
        name: user.name,
        email: user.email
      });
    }
  }

  private validateStock(): boolean {
    const cartItems = this.cart.getCart();
    for (const item of cartItems) {
      if (item.quantity > item.product.stock) {
        this.toast.show(`${item.product.name} only has ${item.product.stock} in stock`, 'error');
        return false;
      }
    }
    return true;
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.toast.show('Please fill all required fields', 'error');
      return;
    }

    if (!this.validateStock()) return;

    const userId = this.auth.getUserId();
    if (!userId) {
      this.toast.show('You must be logged in to place order', 'error');
      this.router.navigate(['/login']);
      return;
    }

    if (this.cartItems.length === 0) {
      this.toast.show('Your cart is empty', 'error');
      this.router.navigate(['/cart']);
      return;
    }

    const items = this.cartItems.map(item => ({
      productId: item.product.productId,
      quantity: item.quantity
    }));

    const orderDTO: OrderDTO = { userId, items };
    this.loading = true;

    this.order.createOrder(orderDTO).subscribe({
      next: (response) => {
        this.cart.clearCart();
        // Store order details for success page
        const orderSummary = {
          orderId: response.orderId,
          totalAmount: this.total,
          paymentMethod: this.form.value.paymentMethod
        };
        localStorage.setItem('lastOrder', JSON.stringify(orderSummary));
        this.router.navigate(['/order-success']);
      },
      error: (err) => {
        console.error('Order error:', err);
        let errorMsg = 'Failed to place order. ';
        if (err.error && typeof err.error === 'string') errorMsg += err.error;
        else if (err.message) errorMsg += err.message;
        this.toast.show(errorMsg, 'error');
        this.loading = false;
      }
    });
  }
}