import { Component, inject, OnInit, OnDestroy, HostListener } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart-service';
import { ToastService } from '../../services/toast-service';
import { Subscription } from 'rxjs';
import { skip } from 'rxjs/operators';
import { AdminDashboardService } from '../../services/admin-dashboard.service';

@Component({
  selector: 'app-navbar-component',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar-component.html',
  styleUrls: ['./navbar-component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
  auth = inject(AuthService);
  cartService = inject(CartService);
  router = inject(Router);
  private toast = inject(ToastService);
  private adminHub = inject(AdminDashboardService);
  cartCount = 0;
  isMenuOpen = false;
  userDropdownOpen = false;
  private cartSub!: Subscription;

  ngOnInit() {
    // Do not show pre-existing stored count on initial app load.
    this.cartCount = 0;
    this.cartSub = this.cartService.cart$.pipe(skip(1)).subscribe(() => {
      this.cartCount = this.cartService.getCartCount();
    });
    if (this.auth.isAdmin()) {
      this.adminHub.preload();
    }
  }

  prefetchAdminDashboard(): void {
    if (this.auth.isAdmin()) {
      this.adminHub.preload();
    }
  }

  ngOnDestroy() {
    if (this.cartSub) this.cartSub.unsubscribe();
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  toggleUserDropdown(event: Event) {
    event.stopPropagation();
    this.userDropdownOpen = !this.userDropdownOpen;
  }

  @HostListener('document:click', ['$event'])
  closeDropdownOnOutsideClick(event: Event) {
    const target = event.target as HTMLElement;
    if (!target.closest('.dropdown')) {
      this.userDropdownOpen = false;
    }
  }

  getUserDisplayName(): string {
    const user = this.auth.getCurrentUser();
    if (user?.name) return user.name;
    if (user?.email) return user.email.split('@')[0];
    return 'User';
  }

  logout() {
    this.auth.logout();
    this.toast.show('Logged out successfully', 'success');
    this.router.navigate(['/home']);
    this.userDropdownOpen = false;
  }
}
