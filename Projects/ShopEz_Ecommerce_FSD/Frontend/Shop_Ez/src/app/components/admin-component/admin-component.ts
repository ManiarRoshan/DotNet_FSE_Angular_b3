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
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ObjectUrlPipe } from '../../pipes/object-url-pipe';
import { UserAdminService, UserAdmin } from '../../services/user-admin.service';
import { AdminDashboardService } from '../../services/admin-dashboard.service';
import { sortAdminProducts } from '../../utils/product.utils';


@Component({
  selector: 'app-admin-component',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ObjectUrlPipe],
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
  private http = inject(HttpClient);
  private userAdminService = inject(UserAdminService);
  private adminHub = inject(AdminDashboardService);

  showModal = false;
  editMode = false;
  editId = 0;
  uploading = false;
  tab: 'products' | 'orders' | 'users' = 'products';
  products: Product[] = [];
  orders: Order[] = [];
  users: UserAdmin[] = [];
  usersLoading = false;
  updatingUserId: number | null = null;

  form = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0, [Validators.required, Validators.min(1)]],
    stock: [0, [Validators.required, Validators.min(0)]],
    imageUrl: [''],
    category: ['', Validators.required]
  });

  selectedFile: File | null = null;

  ngOnInit() {
    if (!this.auth.isAdmin()) {
      this.router.navigate(['/home']);
      return;
    }
    this.loadProducts();
    this.loadOrders();
  }

  loadProducts() {
    const cached = this.adminHub.snapshot?.products;
    if (cached?.length) {
      this.products = sortAdminProducts(cached);
      this.cdr.detectChanges();
    }

    this.ps.getProductsForAdmin().subscribe({
      next: (p) => {
        this.products = sortAdminProducts(p);
        this.adminHub.updateCache(p, this.adminHub.snapshot?.orders);
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Load products error', err);
        if (!cached?.length) {
          this.toast.show('Failed to load products', 'error');
        }
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
    this.selectedFile = null;
    this.form.reset({ name: '', description: '', price: 0, stock: 0, imageUrl: '', category: '' });
    this.showModal = true;
  }

  edit(p: Product) {
    this.editMode = true;
    this.editId = p.productId;
    this.selectedFile = null;
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

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length) {
      this.selectedFile = input.files[0];
    }
  }

  async uploadImage(): Promise<string | null> {
    if (!this.selectedFile) return this.form.value.imageUrl || null;

    const formData = new FormData();
    formData.append('image', this.selectedFile);

    try {
      const response = await this.http.post<{ imageUrl: string }>(
        `${environment.apiUrl}/images/upload`,
        formData
      ).toPromise();
      return response?.imageUrl || null;
    } catch (err) {
      console.error('Image upload failed', err);
      this.toast.show('Image upload failed', 'error');
      return null;
    }
  }

  async save() {
    if (this.form.invalid) {
      this.toast.show('Please fill all required fields', 'error');
      return;
    }

    let imageUrl = this.form.value.imageUrl || '';
    if (this.selectedFile) {
      this.uploading = true;
      const uploadedUrl = await this.uploadImage();
      this.uploading = false;
      if (uploadedUrl) {
        imageUrl = uploadedUrl;
      } else if (!this.editMode) {
        return;
      }
    }

    const dto: ProductDTO = {
      name: this.form.value.name?.trim() || '',
      description: this.form.value.description?.trim() || '',
      price: Number(this.form.value.price) || 0,
      stock: Number(this.form.value.stock) || 0,
      imageUrl: imageUrl,
      category: this.form.value.category?.trim() || ''
    };

    if (!dto.name || !dto.description || dto.price <= 0 || dto.stock < 0 || !dto.category) {
      this.toast.show('All fields (including category) are required', 'error');
      return;
    }

    if (this.editMode) {
      const updatedName = dto.name;
      this.ps.updateProduct(this.editId, dto).subscribe({
        next: () => {
          this.toast.show(`Product updated: ${updatedName}`, 'success');
          this.loadProducts();
          this.closeModal();
        },
        error: (err) => {
          console.error('Update error', err);
          this.toast.show('Update failed: ' + (err.error?.message || err.message), 'error');
        }
      });
    } else {
      const addedName = dto.name;
      this.ps.addProduct(dto).subscribe({
        next: () => {
          this.toast.show(`Product added: ${addedName} (shown at top)`, 'success');
          this.loadProducts();
          this.closeModal();
        },
        error: (err) => {
          console.error('Add error', err);
          let msg = 'Add failed. ';
          if (err.status === 400) msg += err.error?.message || 'Bad request.';
          else if (err.status === 401) msg += 'Please login again as admin.';
          else msg += err.message;
          this.toast.show(msg, 'error');
        }
      });
    }
  }

  // Method used in template for product list images
  getImageUrl(product: Product): string {
    return this.imageService.getFullImageUrl(product.imageUrl);
  }

  // For displaying current image preview in modal
  getFullImageUrl(url: string): string {
    return this.imageService.getFullImageUrl(url);
  }

  formatOrderDisplayId(orderId: number): string {
    return String(Math.floor(orderId)).padStart(5, '0');
  }

  orderDateValue(order: Order): Date {
    const raw = order.orderDate;
    if (!raw) return new Date();
    const t = String(raw).trim();
    if (/Z|[+-]\d{2}:?\d{2}$/.test(t)) return new Date(t);
    if (t.includes('T')) return new Date(t + 'Z');
    return new Date(t);
  }

  deleteProduct(p: Product) {
    if (!confirm(`Remove "${p.name}" from store? Customers will see it as unavailable.`)) return;
    this.ps.deleteProduct(p.productId).subscribe({
      next: () => {
        this.toast.show(`Product "${p.name}" removed from store`, 'success');
        this.loadProducts();
      },
      error: (err) => {
        this.toast.show('Delete failed: ' + (err.error?.message || err.message), 'error');
      }
    });
  }

  loadUsers() {
    this.usersLoading = true;
    this.userAdminService.getUsers().subscribe({
      next: (users) => {
        this.users = users;
        this.usersLoading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.usersLoading = false;
        this.toast.show('Failed to load users', 'error');
        this.cdr.detectChanges();
      }
    });
  }

  toggleUserRole(u: UserAdmin) {
    const target = u.role === 'Admin' ? 'Customer' : 'Admin';
    if (!confirm(`Change ${u.email} role to ${target}?`)) return;
    this.updatingUserId = u.userId;
    this.userAdminService.toggleRole(u.userId).subscribe({
      next: (res) => {
        u.role = res.role;
        this.toast.show(res.message || `Role updated to ${res.role}`, 'success');
        this.updatingUserId = null;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.updatingUserId = null;
        this.toast.show('Role update failed: ' + (err.error?.message || err.message), 'error');
        this.cdr.detectChanges();
      }
    });
  }

  closeModal() {
    this.showModal = false;
    this.selectedFile = null;
    this.form.reset();
  }

}
