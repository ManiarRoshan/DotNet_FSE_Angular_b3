# ShopEZ - Angular Frontend Setup Guide

## Project Overview
ShopEZ is a full-stack e-commerce application with Angular frontend and ASP.NET Core Web API backend.

## Prerequisites
- Node.js (v18 or higher)
- Angular CLI
- ASP.NET Core Web API backend running at https://localhost:7259
- SQL Server with ECommerceDb database

## Installation Steps

### 1. Install Dependencies
```bash
cd Shop_Ez
npm install
```

### 2. Configure Backend URL
The API base URL is configured in the service files:
- `src/app/services/auth.service.ts` - https://localhost:7259/api/auth
- `src/app/services/product.service.ts` - https://localhost:7259/api/products
- `src/app/services/order.service.ts` - https://localhost:7259/api/orders

If your backend is running on a different URL, update these files.

### 3. Run Database Script
Execute the `products-data.sql` file in your SQL Server to populate 35 products:
```sql
USE ECommerceDb;
GO
-- Run the contents of products-data.sql
```

### 4. Start the Application
```bash
ng serve
```
The application will be available at http://localhost:4200

## Project Structure

```
src/app/
├── components/          # Standalone Angular components
│   ├── home/           # Home page with featured products
│   ├── products/       # Product listing page
│   ├── product-details/# Individual product details
│   ├── cart/           # Shopping cart
│   ├── checkout/       # Checkout page with order form
│   ├── login/          # User login
│   ├── register/       # User registration
│   ├── admin/          # Admin dashboard (protected)
│   ├── orders/         # User order history
│   └── navbar/         # Navigation bar
├── services/           # API services
│   ├── auth.service.ts     # Authentication (JWT)
│   ├── product.service.ts  # Product CRUD
│   ├── cart.service.ts     # Cart management (localStorage)
│   └── order.service.ts    # Order management
├── models/             # TypeScript interfaces
│   ├── product.model.ts
│   ├── user.model.ts
│   ├── order.model.ts
│   └── cart.model.ts
├── guards/             # Route guards
│   ├── auth.guard.ts       # Require login
│   └── admin.guard.ts      # Require admin role
├── app.routes.ts       # Routing configuration
├── app.config.ts       # App configuration
└── app.ts              # Root component
```

## Features Implemented

### Customer Features
- ✅ User Registration
- ✅ User Login (JWT authentication)
- ✅ View Product List
- ✅ View Product Details
- ✅ Add to Cart
- ✅ Remove from Cart
- ✅ Update Cart Quantity
- ✅ Place Order
- ✅ View Order History

### Admin Features
- ✅ Add Product
- ✅ Edit Product
- ✅ Delete Product
- ✅ View All Orders
- ✅ Role-based Access Control (Admin only)

### Technical Features
- ✅ Standalone Angular Components (Angular 21)
- ✅ Reactive Forms for validation
- ✅ Route Guards for protection
- ✅ LocalStorage for cart persistence
- ✅ JWT Token handling
- ✅ Responsive design
- ✅ Toast notifications
- ✅ Error handling

## Authentication Flow

1. User registers via `/register` endpoint
2. User logs in via `/login` endpoint
3. JWT token is stored in localStorage
4. Token is attached to API headers for protected routes
5. User role is checked for admin access

## Admin Access

To access the admin panel:
1. Register a user with role "Admin" (via backend or update database)
2. Or update existing user role in database:
```sql
UPDATE Users SET Role = 'Admin' WHERE Email = 'your-email@example.com'
```
3. Login with admin credentials
4. Access `/admin` route

## API Endpoints Used

### Authentication
- POST `/api/auth/register` - Register new user
- POST `/api/auth/login` - Login and get JWT token

### Products
- GET `/api/products` - Get all products (public)
- GET `/api/products/{id}` - Get product by ID (public)
- POST `/api/products` - Add product (Admin only)
- PUT `/api/products/{id}` - Update product (Admin only)
- DELETE `/api/products/{id}` - Delete product (Admin only)

### Orders
- POST `/api/orders` - Create order (Authenticated)
- GET `/api/orders` - Get all orders (Admin only)
- GET `/api/orders/{id}` - Get order by ID (Public)

## Products Data

The `products-data.sql` file includes 35 products across categories:
- **Electronics** (10): Laptops, phones, cameras, gaming consoles
- **Accessories** (10): Chargers, cases, cables, adapters
- **Peripherals** (5): Keyboards, mice, monitors, tablets
- **Mobiles** (5): Smartphones from various brands
- **Smart Home** (5): Smart speakers, plugs, displays

All products include:
- High-quality Unsplash images
- Realistic pricing in INR
- Stock quantities
- Detailed descriptions

## Troubleshooting

### CORS Issues
If you face CORS errors, ensure your backend allows CORS for http://localhost:4200

### SSL Certificate Issues
If backend uses HTTPS with self-signed certificate, you may need to:
1. Trust the certificate in your browser
2. Or run backend with HTTP for development

### Cart Not Persisting
Cart uses localStorage. Ensure cookies are enabled in your browser.

### Admin Access Denied
Check that:
1. User is logged in
2. User role is exactly "Admin" (case-sensitive)
3. JWT token is valid and not expired

## Development Notes

- All components are standalone (no NgModule)
- Uses Angular 21 with latest features
- Cart is stored in localStorage for persistence
- JWT tokens stored in localStorage
- Images are loaded from Unsplash URLs
- All prices in Indian Rupees (₹)

## Future Enhancements

- User profile management
- Product search and filtering
- Product reviews and ratings
- Wishlist feature
- Order tracking
- Payment gateway integration
- Email notifications
- Password reset functionality

## Support

For issues or questions, refer to the backend API documentation or check the browser console for error messages.
