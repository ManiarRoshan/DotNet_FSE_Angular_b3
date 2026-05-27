import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product-service';
import { CartService } from '../../services/cart-service';
import { ToastService } from '../../services/toast-service';
import { ImageService } from '../../services/image-service';
import { AuthService } from '../../services/auth.service';
import { Product } from '../../models/product.model';
import { PageBackButtonComponent } from '../page-back-button/page-back-button';
import { filterCatalogProducts, isProductDeleted } from '../../utils/product.utils';

@Component({
  selector: 'app-products-component',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule, PageBackButtonComponent],
  templateUrl: './products-component.html',
  styleUrls: ['./products-component.css']
})
export class ProductsComponent implements OnInit {
  private ps = inject(ProductService);
  private cs = inject(CartService);
  private toast = inject(ToastService);
  private cdr = inject(ChangeDetectorRef);
  private imageService = inject(ImageService);
  private auth = inject(AuthService);

  allProducts: Product[] = [];
  displayedProducts: Product[] = [];
  loading = false;
  showLoadMore = false;
  selectedCategory = '';
  searchTerm = '';
  sortBy: 'default' | 'priceAsc' | 'priceDesc' = 'default';

  categories = ['Electronics', 'Accessories', 'Mobiles', 'Smart Home', 'All'];

  ngOnInit() {
    this.loadProducts();
  }

  getImageUrl(product: Product): string {
    return this.imageService.getFullImageUrl(product.imageUrl);
  }

  loadProducts() {
    this.loading = true;
    this.ps.getProducts().subscribe({
      next: (products) => {
        this.allProducts = products;
        this.applyFilter();
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.toast.show('Failed to load products', 'error');
      }
    });
  }

  private getFilteredProducts(): Product[] {
    const filtered = filterCatalogProducts(this.allProducts, {
      searchTerm: this.searchTerm,
      category: this.selectedCategory || undefined
    });
    return this.sortProducts(filtered);
  }

  applyFilter() {
    const filtered = this.getFilteredProducts();
    this.displayedProducts = filtered.slice(0, 6);
    this.showLoadMore = filtered.length > 6;
  }

  private sortProducts(list: Product[]): Product[] {
    const copy = [...list];
    if (this.sortBy === 'priceAsc') {
      copy.sort((a, b) => a.price - b.price);
    } else if (this.sortBy === 'priceDesc') {
      copy.sort((a, b) => b.price - a.price);
    }
    return copy;
  }

  loadMore() {
    this.displayedProducts = this.getFilteredProducts();
    this.showLoadMore = false;
  }

  onSortChange() {
    this.applyFilter();
  }

  filterByCategory(category: string) {
    this.selectedCategory = category;
    this.applyFilter();
  }

  clearFilter() {
    this.selectedCategory = '';
    this.searchTerm = '';
    this.sortBy = 'default';
    this.applyFilter();
  }

  onSearchChange() {
    this.applyFilter();
  }

  isUnavailable(product: Product): boolean {
    return isProductDeleted(product);
  }

  addToCart(product: Product) {
    if (!this.auth.isLoggedIn()) {
      this.toast.show('Please login to add items to cart', 'info');
      return;
    }
    if (this.isUnavailable(product)) {
      this.toast.show('This item is no longer available to buy', 'error');
      return;
    }
    const cartItem = this.cs.getCart().find(item => item.product.productId === product.productId);
    const currentQty = cartItem ? cartItem.quantity : 0;
    if (currentQty + 1 > product.stock) {
      this.toast.show(`Only ${product.stock} in stock`, 'error');
      return;
    }
    this.cs.addToCart(product);
    this.toast.show(`${product.name} added to cart!`, 'success');
  }
}
