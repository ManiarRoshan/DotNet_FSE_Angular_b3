import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product-service';
import { ToastService } from '../../services/toast-service';
import { CartService } from '../../services/cart-service';
import { ImageService } from '../../services/image-service';
import { AuthService } from '../../services/auth.service';
import { PageBackButtonComponent } from '../page-back-button/page-back-button';

@Component({
  selector: 'app-product-details-component',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, PageBackButtonComponent],
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
  private auth = inject(AuthService);

  product: Product | null = null;
  quantity = 1;
  loading = false;
  lightboxOpen = false;
  displayReviews: { author: string; rating: number; text: string; date: string }[] = [];

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
        this.quantity = 1;
        this.displayReviews = this.buildReviews(p);
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

  openLightbox(): void {
    this.lightboxOpen = true;
  }

  closeLightbox(event?: Event): void {
    event?.stopPropagation();
    this.lightboxOpen = false;
  }

  starRow(rating: number): string {
    const n = Math.max(0, Math.min(5, Math.round(rating)));
    return '★'.repeat(n) + '☆'.repeat(5 - n);
  }

  private buildReviews(p: Product): { author: string; rating: number; text: string; date: string }[] {
    const base = p.productId * 7 + p.name.length;
    const pool = [
      { author: 'Rajiv', rating: 5, text: 'Great quality and fast delivery. Packaging was neat.', date: '2 weeks ago' },
      { author: 'Priya S.', rating: 4, text: 'Matches the description. Good value for the price.', date: '1 month ago' },
      { author: 'Rahul M.', rating: 5, text: 'Very happy with this purchase. Would recommend.', date: '3 days ago' },
      { author: 'Ananya K.', rating: 4, text: 'Solid product. Minor shipping delay but worth it.', date: '5 days ago' }
    ];
    const count = 2 + (base % 2);
    return pool.slice(0, count);
  }

  addToCart() {
    if (!this.product) return;
    if (!this.auth.isLoggedIn()) {
      this.toast.show('Please login to add items to cart', 'info');
      return;
    }
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
