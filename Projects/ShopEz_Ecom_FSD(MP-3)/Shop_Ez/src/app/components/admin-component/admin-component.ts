import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../services/product-service';
import { OrderService } from '../../services/order-service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Product, ProductDTO } from '../../models/product.model';
import { Order } from '../../models/order.model';
import { ToastService } from '../../services/toast-service';
import { ImageService } from '../../services/image-service';

@Component({
  selector: 'app-admin-component',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-component.html',
  styleUrls: ['./admin-component.css']
})
export class AdminComponent implements OnInit {
  private ps = inject(ProductService);
  private os = inject(OrderService);
  private auth = inject(AuthService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private toast = inject(ToastService);
  private cdr = inject(ChangeDetectorRef);
  private imageService = inject(ImageService);

  showDeleteModal = false;
  deleteProductId: number | null = null;
  deleteProductName = '';

  tab: 'products' | 'orders' = 'products';
  products: Product[] = [];
  orders: Order[] = [];
  showModal = false;
  editMode = false;
  editId = 0;

  form = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(1)]],
    stock: [0, [Validators.required, Validators.min(0)]],
    imageUrl: [''],
    category: ['', Validators.required]
  });

  ngOnInit() {
    if (!this.auth.isAdmin()) {
      this.router.navigate(['/home']);
      return;
    }
    this.loadProducts();
    this.loadOrders();
  }

  loadProducts() {
    this.ps.getProducts().subscribe({
      next: (p) => {
        this.products = p;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Load products error', err);
        this.toast.show('Failed to load products', 'error');
      }
    });
  }

  loadOrders() {
    this.os.getAllOrders().subscribe({
      next: (o) => {
        this.orders = o;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Load orders error', err);
        this.toast.show('Failed to load orders', 'error');
      }
    });
  }

  openModal() {
    this.editMode = false;
    this.editId = 0;
    this.form.reset({ name: '', description: '', price: 0, stock: 0, imageUrl: '', category: '' });
    this.showModal = true;
  }

  edit(p: Product) {
    this.editMode = true;
    this.editId = p.productId;
    this.form.patchValue({
      name: p.name,
      description: p.description,
      price: p.price,
      stock: p.stock,
      imageUrl: p.imageUrl,
      category: p.category || ''
    });
    this.showModal = true;
  }

  getImageUrl(product: Product): string {
    return this.imageService.getFullImageUrl(product.imageUrl);
  }

  closeModal() {
    this.showModal = false;
    this.form.reset();
  }

  save() {
    if (this.form.invalid) {
      this.toast.show('Please fill all required fields', 'error');
      return;
    }

    const dto: ProductDTO = {
      name: this.form.value.name?.trim() || '',
      description: this.form.value.description?.trim() || '',
      price: Number(this.form.value.price) || 0,
      stock: Number(this.form.value.stock) || 0,
      imageUrl: this.form.value.imageUrl?.trim() || '',
      category: this.form.value.category?.trim() || ''
    };

    // Debug log
    console.log('Saving product DTO:', dto);

    if (!dto.name || !dto.description || dto.price <= 0 || dto.stock < 0 || !dto.category) {
      this.toast.show('All fields (including category) are required', 'error');
      return;
    }

    if (this.editMode) {
      this.ps.updateProduct(this.editId, dto).subscribe({
        next: () => {
          this.toast.show('Product updated', 'success');
          this.loadProducts();
          this.closeModal();
        },
        error: (err) => {
          console.error('Update error', err);
          this.toast.show('Update failed: ' + (err.error?.message || err.message), 'error');
        }
      });
    } else {
      // ADD PRODUCT
      this.ps.addProduct(dto).subscribe({
        next: (response) => {
          console.log('Add product response:', response);
          this.toast.show('Product added successfully', 'success');
          this.loadProducts();
          this.closeModal();
        },
        error: (err) => {
          console.error('Add error', err);
          let msg = 'Add failed. ';
          if (err.status === 400) {
            msg += err.error?.message || err.error?.title || 'Bad request. Check category and fields.';
          } else if (err.status === 401) {
            msg += 'Please login again as admin.';
          } else {
            msg += err.message;
          }
          this.toast.show(msg, 'error');
        }
      });
    }
  }

  confirmDelete(id: number, name: string) {
    this.deleteProductId = id;
    this.deleteProductName = name;
    this.showDeleteModal = true;
  }

  deleteProduct() {
    if (this.deleteProductId === null) return;
    this.ps.deleteProduct(this.deleteProductId).subscribe({
      next: () => {
        this.toast.show('Product deleted', 'success');
        this.loadProducts();
        this.closeDeleteModal();
      },
      error: (err) => {
        console.error('Delete error', err);
        this.toast.show('Delete failed', 'error');
        this.closeDeleteModal();
      }
    });
  }

  closeDeleteModal() {
    this.showDeleteModal = false;
    this.deleteProductId = null;
    this.deleteProductName = '';
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/home']);
  }
}