import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product-service';
import { ToastService } from '../../services/toast-service';
import { CartService } from '../../services/cart-service';
import { ImageService } from '../../services/image-service';

@Component({
  selector: 'app-product-details-component',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './product-details-component.html',
  styleUrls: ['./product-details-component.css']
})
export class ProductDetailsComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private ps = inject(ProductService);
  private cs = inject(CartService);
  private toast = inject(ToastService);
  private cdr = inject(ChangeDetectorRef);
  private imageService = inject(ImageService);

  product: Product | null = null;
  quantity = 1;
  loading = false;

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id && !isNaN(id)) this.loadProduct(id);
  }

  getImageUrl(): string {
    if (!this.product) return 'assets/images/default-product.svg';
    return this.imageService.getFullImageUrl(this.product.imageUrl);
  }

  loadProduct(id: number) {
    this.loading = true;
    this.ps.getProduct(id).subscribe({
      next: (p) => {
        this.product = p;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.toast.show('Product not found', 'error');
        this.cdr.detectChanges();
      }
    });
  }

  addToCart() {
    if (!this.product) return;
    const cartItem = this.cs.getCart().find(item => item.product.productId === this.product!.productId);
    const currentQty = cartItem ? cartItem.quantity : 0;
    const desiredQty = currentQty + this.quantity;
    if (desiredQty > this.product.stock) {
      this.toast.show(`Cannot add ${this.quantity} more. Only ${this.product.stock - currentQty} left.`, 'error');
      return;
    }
    for (let i = 0; i < this.quantity; i++) this.cs.addToCart(this.product);
    this.toast.show(`${this.quantity} x ${this.product.name} added!`, 'success');
  }
}