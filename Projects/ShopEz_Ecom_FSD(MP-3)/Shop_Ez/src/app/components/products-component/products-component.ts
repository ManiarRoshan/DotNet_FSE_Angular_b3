import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product-service';
import { CartService } from '../../services/cart-service';
import { ToastService } from '../../services/toast-service';
import { ImageService } from '../../services/image-service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-products-component',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './products-component.html',
  styleUrls: ['./products-component.css']
})
export class ProductsComponent implements OnInit {
  private ps = inject(ProductService);
  private cs = inject(CartService);
  private toast = inject(ToastService);
  private cdr = inject(ChangeDetectorRef);
  private imageService = inject(ImageService);

  allProducts: Product[] = [];
  displayedProducts: Product[] = [];
  loading = false;
  showLoadMore = false;
  selectedCategory = '';
  searchTerm = '';

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

  applyFilter() {
    let filtered = this.allProducts;
    if (this.selectedCategory && this.selectedCategory !== 'All') {
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
    if (this.selectedCategory && this.selectedCategory !== 'All') {
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