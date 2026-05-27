# ShopEZ — Frontend

**Project Owner:** Maniar Roshan
**Technology:** Angular 19 | TypeScript | Bootstrap 5 | JWT Authentication
**Status:** Production Ready | Dockerized

---

## Overview

ShopEZ is a full-stack E-Commerce web application. This repository contains the Angular 19 frontend (Single Page Application) that communicates with the ShopEZ microservices backend through an Ocelot API Gateway.

---

## Technology Stack

| Technology | Version |
|---|---|
| Angular | 19 (Standalone Components) |
| TypeScript | 5.x |
| Bootstrap | 5.3 |
| Node.js | 20+ |
| Angular CLI | 19.x |
| Testing | Karma + Jasmine |
| Build/Serve | Docker + Nginx (production) |

---

## Project Structure

```
src/
├── app/
│   ├── components/
│   │   ├── home/                        # Landing page
│   │   ├── navbar-component/            # Top navigation bar
│   │   ├── footer/                      # Footer
│   │   ├── products-component/          # Product catalogue listing
│   │   ├── product-details-component/   # Single product view
│   │   ├── cart-component/              # Cart management (auth protected)
│   │   ├── checkout-component/          # Checkout form (auth protected)
│   │   ├── orders-component/            # Order history (auth protected)
│   │   ├── order-success-component/     # Post-order confirmation
│   │   ├── login-component/             # Customer login
│   │   ├── register-component/          # Customer registration
│   │   ├── admin-login-component/       # Admin login portal
│   │   ├── admin-dashboard-component/   # Admin stats dashboard (admin only)
│   │   ├── admin-component/             # Admin panel: products, users, orders
│   │   ├── toast-component/             # Global notification overlay
│   │   └── page-back-button/            # Reusable back button
│   ├── services/                        # HTTP + business logic services
│   ├── guards/                          # authGuard, adminGuard
│   ├── interceptors/                    # authInterceptor (JWT attachment)
│   ├── models/                          # TypeScript interfaces
│   ├── pipes/                           # ObjectUrlPipe
│   ├── utils/                           # product.utils.ts
│   ├── app.ts                           # Root component
│   ├── app.routes.ts                    # Route definitions
│   └── app.config.ts                    # Global providers
├── assets/images/products/              # Static product images
├── environments/                        # API URL config
│   ├── environment.ts                   # Development: localhost:5000
│   └── environment.prod.ts              # Production
├── styles.css                           # Global CSS
└── index.html
```

---

## Routes

| Path | Component | Guard |
|---|---|---|
| `/home` | HomeComponent | Public |
| `/products` | ProductsComponent | Public |
| `/product/:id` | ProductDetailsComponent | Public |
| `/login` | LoginComponent | Public |
| `/register` | RegisterComponent | Public |
| `/admin-login` | AdminLoginComponent | Public |
| `/cart` | CartComponent | authGuard |
| `/checkout` | CheckoutComponent | authGuard |
| `/orders` | OrdersComponent | authGuard |
| `/order-success` | OrderSuccessComponent | Public |
| `/admin-dashboard` | AdminDashboardComponent | adminGuard |
| `**` | → /home | Redirect |

---

## Services

| Service | Responsibility |
|---|---|
| `AuthService` | Login, register, JWT decode, token storage, role check |
| `ProductService` | Fetch all products, search with filters, fetch by ID |
| `CartService` | Get cart, add/remove/update items |
| `OrderService` | Create order, get my orders |
| `ImageService` | Upload product image (multipart POST) |
| `AdminDashboardService` | Fetch admin stats |
| `UserAdminService` | Fetch all users, toggle role |
| `ToastService` | Global success/error notifications |

---

## Authentication Flow

1. User submits credentials → `AuthService.login()` → POST `/api/auth/login`
2. JWT token received → decoded (Base64) → userId, name, email, role extracted
3. Token stored in `localStorage` as `'token'`
4. `BehaviorSubject authState$` emits `true` → Navbar updates reactively
5. `authInterceptor` attaches `Authorization: Bearer <token>` to all protected requests
6. Token expiry checked on every `isLoggedIn()` call → auto-logout if expired

---

## Guards

**authGuard** — checks `AuthService.isLoggedIn()`. Redirects to `/login` if not authenticated.

**adminGuard** — checks `isLoggedIn()` AND `isAdmin()`. Redirects to `/admin-login` if not Admin.

---

## Getting Started

### Prerequisites

- Node.js 20+
- Angular CLI 19 (`npm install -g @angular/cli`)
- Backend services running (or Docker Compose)

### Local Development

```bash
# Install dependencies
npm install

# Start dev server
ng serve --open
# App available at: http://localhost:4200
```

### Production Build

```bash
ng build
# Output in: dist/Shop_Ez/browser/
```

### Run Tests

```bash
# Unit tests with Karma
ng test

# With code coverage
ng test --code-coverage
```

---

## Docker Deployment

The frontend is included in the full Docker Compose stack.

```bash
# From Backend/Ecommerce_MS_Web_API directory:
docker compose up --build

# Frontend available at: http://localhost:4200
# API Gateway at:        http://localhost:5000
```

### Frontend Dockerfile (multi-stage)

```dockerfile
# Stage 1: Build Angular
FROM node:20-alpine AS builder
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

# Stage 2: Serve with Nginx
FROM nginx:alpine
COPY --from=builder /app/dist/Shop_Ez/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

---

## Environment Configuration

**Development** (`src/environments/environment.ts`):
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

**Production** (`src/environments/environment.prod.ts`):
```typescript
export const environment = {
  production: true,
  apiUrl: 'http://localhost:5000/api'
};
```

Update `apiUrl` to your cloud endpoint before deploying to production.

---

## API Communication

All API calls go through the Ocelot Gateway at `http://localhost:5000`. The `authInterceptor` automatically attaches `Bearer` tokens to protected requests and handles 401 responses by logging out the user.

| Angular Service | Gateway Route | Backend Service |
|---|---|---|
| AuthService | `/api/auth/...` | UserService :5001 |
| ProductService | `/api/products/...` | ProductService :5002 |
| ImageService | `/api/images/upload` | ProductService :5002 |
| CartService | `/api/cart/...` | CartService :5003 |
| OrderService | `/api/orders/...` | OrderService :5004 |

---

## Key Design Decisions

- **Standalone Components** — No NgModules; each component uses `standalone: true` for cleaner architecture
- **Functional Guards** — Modern Angular 16+ functional guard pattern (no class-based guards)
- **Functional Interceptor** — `withInterceptors([authInterceptor])` registered via `provideHttpClient`
- **RxJS BehaviorSubject** — Used for auth state without NgRx overhead
- **No external state library** — Keeps complexity low for a project of this scale

---

## Project Owner

**Maniar Roshan**
ShopEZ E-Commerce Application — May 2026
