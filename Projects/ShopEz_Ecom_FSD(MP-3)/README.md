# Extract The Zip File and Open the Project in Visual Studio Code or Later


# ShopEZ - E-Commerce Application

ShopEZ is a full-stack e-commerce web application built with Angular 17+ (standalone components) on the frontend and ASP.NET Core 8 Web API on the backend. It provides separate experiences for customers and administrators, with JWT authentication, shopping cart, order placement, and full product management (CRUD).

---

## Features

### Customer Features

- Browse all products with load more pagination (initially 6 products)
- View product details with stock-aware quantity selector
- Filter products by category (Electronics, Accessories, Mobiles, Smart Home) with a Clear Filter button
- Add products to cart — stock validation prevents adding more than available
- Cart count badge in navbar updates immediately
- Place orders — stock is reduced automatically
- View order history (my orders) with product details
- User registration and login
- Toast notifications for all actions (add to cart, order placed, errors)

### Admin Features

- Admin login at /admin-login (credentials seeded automatically)
- Admin dashboard with two tabs: Products (CRUD) and Orders (view all)
- Add product — modal form with validation
- Edit product — pre-filled modal, updates product details
- Delete product — custom confirmation modal (instead of browser confirm())
- View all customer orders with ordered items
- Access to the same store frontend inside the dashboard (Store tab)

### Technical Highlights

- Standalone Angular components — no NgModules
- Reactive Forms with validation
- JWT authentication — token stored in localStorage, attached via HTTP interceptor
- Route guards — authGuard (requires login) and adminGuard (requires admin role)
- Toast service for non-intrusive user feedback
- Cart service with BehaviorSubject — cart count updates reactively
- Responsive UI — fixed navbar, carousel slideshow on home page, mobile-friendly

---

## Tech Stack

Layer          | Technology
---------------|------------------------------------------------------------
Frontend       | Angular 17 (standalone), RxJS, TypeScript
Backend        | ASP.NET Core 8 Web API, Entity Framework Core
Database       | SQL Server (local)
Auth           | JWT (Bearer)
Styling        | CSS, Bootstrap 5 (carousel, grid, navbar)
HTTP Client    | Angular HttpClient with interceptors

---

## Project Structure (Frontend)

src/
├── app/
│   ├── components/
│   │   ├── home/
│   │   ├── products-component/
│   │   ├── product-details-component/
│   │   ├── cart-component/
│   │   ├── checkout-component/
│   │   ├── orders-component/
│   │   ├── login-component/
│   │   ├── register-component/
│   │   ├── admin-component/
│   │   ├── admin-login-component/
│   │   ├── admin-dashboard-component/
│   │   ├── navbar-component/
│   │   ├── footer/
│   │   └── toast-component/
│   ├── services/
│   ├── guards/
│   ├── interceptors/
│   ├── models/
│   ├── app.routes.ts
│   ├── app.config.ts
│   └── app.component.ts
├── assets/
├── index.html
└── styles.css

---

## Getting Started

### Prerequisites

- Node.js (v18+)
- Angular CLI — npm install -g @angular/cli
- .NET 8 SDK
- SQL Server (Express or Developer edition)

### Backend Setup (ASP.NET Core)

1. Clone the backend repository (or use your existing project).

2. Update the connection string in appsettings.json:

   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceDb;Integrated Security=True;TrustServerCertificate=True"
   }

3. The database is code-first without migrations — DbInitializer seeds data automatically on first run.

4. Run the backend:

   dotnet run

   The API will be available at https://localhost:7259 (or your configured port).

### Frontend Setup (Angular)

1. Navigate to the frontend folder:

   cd Shop_Ez

2. Install dependencies:

   npm install

3. Update the API base URL in services if your backend runs on a different port (default: https://localhost:7259).

4. Start the Angular development server:

   ng serve -o

5. Open http://localhost:4200 in your browser.

---

## Default Admin Credentials

The database is automatically seeded with an admin user:

Email                  | Password   | Role
-----------------------|------------|-------
admin@shopez.com       | admin123   | Admin

- Admin login URL: http://localhost:4200/admin-login
- A link is also available in the footer.
- Regular customers can register via the Register page.

---

## API Endpoints

Method | Endpoint                  | Description                        | Auth Required
-------|---------------------------|------------------------------------|-----------------
POST   | /api/auth/register        | Register a new user                | No
POST   | /api/auth/login           | Login — returns JWT token          | No
GET    | /api/products             | Get all products                   | No
GET    | /api/products/{id}        | Get single product                 | No
POST   | /api/products             | Add new product                    | Admin only
PUT    | /api/products/{id}        | Update product                     | Admin only
DELETE | /api/products/{id}        | Delete product                     | Admin only
POST   | /api/orders               | Place a new order                  | Authenticated user
GET    | /api/orders               | Get all orders (all users)         | Admin only
GET    | /api/orders/myorders      | Get logged-in user's orders        | Authenticated user
GET    | /api/orders/{id}          | Get order by ID                    | Open (demo)

Note: The /myorders endpoint was added to allow customers to fetch only their own orders without frontend-side filtering.

---

## Known Fixes & Improvements

Problem                                  | Solution
|----------------------------------------------------------
Products not loading on first click      | Added ChangeDetectorRef inside loadProducts() to force view update
Home carousel not sliding                | Initialised Bootstrap carousel manually in ngAfterViewInit()
Category filter had no Clear button      | Added a visible Clear Filter button next to category pills
Cart badge not updating after add        | Implemented BehaviorSubject in CartService; navbar subscribes
Admin CRUD — Add Product not working     | Fixed ProductDTO structure, ensured POST request includes all fields
Delete used browser confirm() popup      | Replaced with a custom modal (same style as add/edit)
Orders not visible to customers          | Added /api/orders/myorders endpoint and frontend getMyOrders()
Image placeholders failing               | Replaced via.placeholder.com with placehold.co (reliable)
Admin login not redirecting after fail   | Left as redirect to customer login — improves user experience

---

## License

This project is for educational/demo purposes. Free to use and modify.

---

## Author

Developed as a final project for the .NET Full Stack course.
For any queries, contact: maniarroshan7@gmail.com
