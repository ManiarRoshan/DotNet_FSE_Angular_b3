import { Routes } from '@angular/router';
import { adminGuard } from './guards/admin.guard';
import { authGuard } from './guards/auth.guard';
import { HomeComponent } from './components/home/home';
import { ProductsComponent } from './components/products-component/products-component';
import { ProductDetailsComponent } from './components/product-details-component/product-details-component';
import { CartComponent } from './components/cart-component/cart-component';
import { CheckoutComponent } from './components/checkout-component/checkout-component';
import { LoginComponent } from './components/login-component/login-component';
import { RegisterComponent } from './components/register-component/register-component';
import { AdminComponent } from './components/admin-component/admin-component';
import { OrdersComponent } from './components/orders-component/orders-component';
import { AdminLoginComponent } from './components/admin-login-component/admin-login-component';
import { AdminDashboardComponent } from './components/admin-dashboard-component/admin-dashboard-component';
import { OrderSuccessComponent } from './components/order-success-component/order-success-component';

export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'product/:id', component: ProductDetailsComponent },
  { path: 'cart', component: CartComponent, canActivate: [authGuard] },
  { path: 'checkout', component: CheckoutComponent, canActivate: [authGuard] },
  { path: 'order-success', component: OrderSuccessComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'admin-login', component: AdminLoginComponent },
  { path: 'admin-dashboard', component: AdminDashboardComponent, canActivate: [adminGuard] },
  { path: 'orders', component: OrdersComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '/home' }
];