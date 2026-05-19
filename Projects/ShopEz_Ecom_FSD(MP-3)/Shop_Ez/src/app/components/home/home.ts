import { Component, inject, OnInit, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product-service';
import { CartService } from '../../services/cart-service';
import { ToastService } from '../../services/toast-service';
import { ImageService } from '../../services/image-service';
import { AuthService } from '../../services/auth.service';

declare var bootstrap: any;

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  private productService = inject(ProductService);
  private cartService = inject(CartService);
  private toast = inject(ToastService);
  private cdr = inject(ChangeDetectorRef);
  private imageService = inject(ImageService);
  auth = inject(AuthService);

  featuredProducts: Product[] = [];
  allProducts: Product[] = [];
  displayedProducts: Product[] = [];
  loading = false;
  showLoadMore = false;
  selectedCategory = '';
  searchTerm = '';

  categories = [
    { name: 'Electronics', icon: '💻' },
    { name: 'Accessories', icon: '🎧' },
    { name: 'Mobiles', icon: '📱' },
    { name: 'Smart Home', icon: '🏠' }
  ];

  getImageUrl(product: Product): string {
    return this.imageService.getFullImageUrl(product.imageUrl);
  }

  ngOnInit() {
    this.loadAllProducts();
  }

  ngAfterViewInit() {
    this.initCarousel();
  }

  initCarousel() {
    setTimeout(() => {
      const carouselElement = document.getElementById('productCarousel');
      if (carouselElement && typeof bootstrap !== 'undefined') {
        new bootstrap.Carousel(carouselElement, {
          interval: 3000,
          ride: 'carousel'
        });
      }
    }, 100);
  }

  loadAllProducts() {
    this.loading = true;
    this.productService.getProducts().subscribe({
      next: (products) => {
        this.allProducts = products;
        this.featuredProducts = products.slice(0, 5);
        this.applyFilter();
        this.loading = false;
        this.cdr.detectChanges();
        this.initCarousel();
      },
      error: () => {
        this.loading = false;
        this.cdr.detectChanges();
        this.toast.show('Failed to load products', 'error');
      }
    });
  }

  applyFilter() {
    let filtered = this.allProducts;
    if (this.selectedCategory) {
      filtered = filtered.filter(p =>
        (p.category && p.category === this.selectedCategory) ||
        p.name.toLowerCase().includes(this.selectedCategory.toLowerCase()) ||
        p.description.toLowerCase().includes(this.selectedCategory.toLowerCase())
      );
    }
    if (this.searchTerm.trim()) {
      const term = this.searchTerm.toLowerCase();
      filtered = filtered.filter(p =>
        p.name.toLowerCase().includes(term) ||
        p.description.toLowerCase().includes(term)
      );
    }
    this.displayedProducts = filtered.slice(0, 6);
    this.showLoadMore = filtered.length > 6;
  }

  loadMore() {
    let filtered = this.allProducts;
    if (this.selectedCategory) {
      filtered = filtered.filter(p =>
        (p.category && p.category === this.selectedCategory) ||
        p.name.toLowerCase().includes(this.selectedCategory.toLowerCase()) ||
        p.description.toLowerCase().includes(this.selectedCategory.toLowerCase())
      );
    }
    if (this.searchTerm.trim()) {
      const term = this.searchTerm.toLowerCase();
      filtered = filtered.filter(p =>
        p.name.toLowerCase().includes(term) ||
        p.description.toLowerCase().includes(term)
      );
    }
    this.displayedProducts = filtered;
    this.showLoadMore = false;
  }

  filterByCategory(category: string) {
    this.selectedCategory = category;
    this.searchTerm = '';
    this.applyFilter();
  }

  clearFilter() {
    this.selectedCategory = '';
    this.searchTerm = '';
    this.applyFilter();
  }

  onSearchChange() {
    this.applyFilter();
  }

  addToCart(product: Product) {
    const cartItem = this.cartService.getCart().find(item => item.product.productId === product.productId);
    const currentQty = cartItem ? cartItem.quantity : 0;
    if (currentQty + 1 > product.stock) {
      this.toast.show(`Only ${product.stock} in stock`, 'error');
      return;
    }
    this.cartService.addToCart(product);
    this.toast.show(`${product.name} added to cart!`, 'success');
  }
}