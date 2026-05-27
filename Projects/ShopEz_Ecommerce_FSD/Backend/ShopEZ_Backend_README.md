# ShopEZ — Backend Microservices

**Project Owner:** Maniar Roshan
**Technology:** ASP.NET Core 8 | Microservices | Ocelot Gateway | SQL Server | EF Core | Dapper | Docker
**Status:** Production Ready | Fully Dockerized

---

## Overview

ShopEZ's backend is built on a Microservices Architecture using ASP.NET Core 8. Four independent services handle distinct business domains, all routed through an Ocelot API Gateway. JWT Bearer authentication is enforced at both the Gateway and individual service levels.

---

## Microservices at a Glance

| Service | Local Port | Docker Port | Responsibility |
|---|---|---|---|
| **UserService** | 5001 | 8080 (internal) | Auth: Register, Login, JWT, Profile, User Management |
| **ProductService** | 5002 | 8080 (internal) | Products: CRUD, Search, Image Upload, Stock |
| **CartService** | 5003 | 8080 (internal) | Cart: Add, View, Update Qty, Remove, Clear |
| **OrderService** | 5004 | 8080 (internal) | Orders: Create, History, Cancel |
| **ApiGateway** | 5000 | 5000 (exposed) | Ocelot: Route all requests, JWT validation |

---

## Architecture Overview

```
Angular Frontend (Port 4200)
           │
           ▼
Ocelot API Gateway (Port 5000)
    JWT Validation • Route Forwarding
           │
    ┌──────┼──────────────┬──────────────┐
    ▼              ▼              ▼              ▼
UserService    ProductService  CartService   OrderService
  :5001          :5002          :5003          :5004
    │              │              │              │
    └──────────────┴──────────────┴──────────────┘
                         │
                         ▼
              SQL Server — ECommerceDb
              (Shared database, all services)
```

---

## Solution Structure

```
Ecommerce_MS_Web_API/
├── ApiGateway/
│   ├── Program.cs           # Ocelot + JWT setup
│   ├── Ocelot.json          # Route config (local)
│   ├── Ocelot.Docker.json   # Route config (Docker)
│   └── Dockerfile
├── UserService/
│   ├── Controllers/         # AuthController, UsersController, UserProfileController
│   ├── Services/            # IAuthService, JwtTokenService
│   ├── Repositories/        # IUserRepository, UserRepository
│   ├── Models/              # User entity
│   ├── DTOs/                # LoginDto, RegisterDto, UserProfileDto, UserAdminDto
│   ├── Data/                # UserDbContext (EF Core)
│   ├── Migrations/          # EF Core migrations
│   └── Dockerfile
├── ProductService/
│   ├── Controllers/         # ProductsController, ImagesController
│   ├── Services/            # IProductService, ProductService
│   ├── Repositories/        # IProductReadRepository (Dapper), IProductWriteRepository
│   ├── Models/              # Product entity
│   ├── DTOs/                # ProductDTO, ProductQueryParameters, UpdateStockDto
│   ├── Data/                # DapperContext
│   └── Dockerfile
├── CartService/
│   ├── Controllers/         # CartController
│   ├── Services/            # ICartService, CartServices, IProductApiClient
│   ├── Repositories/        # ICartRepository, CartRepository
│   ├── Models/              # CartItem entity
│   ├── DTOs/                # AddToCartDto, CartResponseDto
│   ├── Data/                # CartDbContext (EF Core)
│   ├── Migrations/
│   └── Dockerfile
├── OrderService/
│   ├── Controllers/         # OrdersController
│   ├── Services/            # IOrderService, OrderService, IProductApiClient
│   ├── Repositories/        # IOrderRepository, OrderRepository
│   ├── Models/              # Order, OrderItem entities
│   ├── DTOs/                # OrderDTO, OrderResponseDto, ProductFromProductService
│   ├── Data/                # OrderDbContext (EF Core)
│   ├── Migrations/
│   └── Dockerfile
├── ApiGateway.Tests/
├── CartService.Tests/
├── OrderService.Tests/
├── Database/
│   └── schema-updates.sql
└── docker-compose.yml
```

---

## API Endpoints

### UserService (`/api/auth`, `/api/users`, `/api/user`)

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/api/auth/register` | No | Register new customer |
| POST | `/api/auth/login` | No | Login — returns JWT token |
| GET | `/api/user/profile` | Bearer | Get own profile |
| PUT | `/api/user/profile` | Bearer | Update own profile |
| GET | `/api/users` | Admin | Get all users |
| PUT | `/api/users/:id/toggle-role` | Admin | Toggle Customer ↔ Admin role |

### ProductService (`/api/products`, `/api/images`)

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/products` | No | Get all active products |
| GET | `/api/products/search` | No | Search with filters (name, category, pagination) |
| GET | `/api/products/:id` | No | Get product by ID |
| POST | `/api/products` | Admin | Add new product |
| PUT | `/api/products/:id` | Admin | Update product |
| DELETE | `/api/products/:id` | Admin | Soft delete product |
| GET | `/api/products/admin/all` | Admin | All products including inactive |
| PUT | `/api/products/:id/stock` | Internal | Update stock quantity |
| POST | `/api/images/upload` | Bearer | Upload product image |
| GET | `/images/:filename` | No | Serve static product image |

### CartService (`/api/cart`)

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| GET | `/api/cart` | Bearer | Get current user's cart |
| POST | `/api/cart` | Bearer | Add item to cart |
| PUT | `/api/cart/:productId` | Bearer | Update item quantity |
| DELETE | `/api/cart/:productId` | Bearer | Remove item from cart |
| DELETE | `/api/cart` | Bearer | Clear entire cart |

### OrderService (`/api/orders`)

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| POST | `/api/orders` | Bearer | Create new order |
| GET | `/api/orders` | Admin | Get all orders |
| GET | `/api/orders/:id` | Any | Get order by ID |
| GET | `/api/orders/myorders` | Bearer | Get current user's orders |
| PUT | `/api/orders/:id/cancel` | Bearer | Cancel order |

---

## Database Design

**Database:** SQL Server — `ECommerceDb`

| Table | Key Columns |
|---|---|
| `Users` | UserId (PK), Name, Email, Password (hashed), Role |
| `Products` | Id (PK), Name, Description, Price, Category, Stock, ImageUrl, IsActive |
| `CartItems` | Id (PK), UserId, ProductId, Quantity |
| `Orders` | OrderId (PK), UserId, OrderDate, TotalAmount, Status, ShippingAddress, City, ZipCode, Phone, PaymentMethod, Notes |
| `OrderItems` | Id (PK), OrderId (FK), ProductId, ProductName, Quantity, Price, ImageUrl |

---

## JWT Authentication

**Token Generation** (UserService):
- Claims: `nameid` (userId), `unique_name` (name), `email`, `role`
- Algorithm: HMACSHA256
- Signed with: `Jwt:Key` from configuration

**Token Validation**:
1. Ocelot Gateway validates Bearer token on protected routes
2. Each individual microservice also validates the token independently

**Configuration** (same across all services):
```json
{
  "Jwt": {
    "Key": "SuperSecretKeyForShopEZProject2026!CheckLength",
    "Issuer": "ShopEZ",
    "Audience": "ShopEZUsers"
  }
}
```

---

## Technology Stack

| Technology | Usage |
|---|---|
| ASP.NET Core 8 | Web API framework |
| C# (.NET 8) | Primary language |
| Entity Framework Core 8 | ORM for schema migrations, write operations |
| Dapper | Micro-ORM for high-performance read queries (ProductService) |
| SQL Server 2022 | Database (Docker container) |
| Ocelot 23.x | API Gateway / reverse proxy |
| JWT Bearer Authentication | Stateless authentication |
| xUnit + Moq | Unit testing + mocking |
| Swagger (Swashbuckle) | Auto-generated API documentation |
| Docker + Docker Compose | Containerization and orchestration |

---

## Getting Started

### Quick Start with Docker (Recommended)

```bash
# 1. Navigate to backend folder
cd Backend/Ecommerce_MS_Web_API

# 2. Build and start all containers
docker compose up --build

# 3. Apply EF Core migrations (run once after first startup)
# (After SQL Server is ready ~30-60 seconds)
dotnet ef database update  # from each service directory

# 4. Seed products (optional)
# Run: Frontend/Shop_Ez/products-data.sql against ECommerceDb

# Access points:
# Frontend:    http://localhost:4200
# API Gateway: http://localhost:5000
# SQL Server:  localhost,14333 (user: sa, password: Your_password123!)
```

### Local Development (Without Docker)

```bash
# Install .NET 8 SDK
# Install SQL Server 2022

# Update appsettings.json ConnectionStrings in each service
# Run each service separately:
cd UserService   && dotnet run   # :5001
cd ProductService && dotnet run  # :5002
cd CartService   && dotnet run   # :5003
cd OrderService  && dotnet run   # :5004
cd ApiGateway    && dotnet run   # :5000
```

### Apply Migrations

```bash
# From each service directory:
dotnet ef database update
```

---

## Docker Compose Services

| Container | Image | Ports | Depends On |
|---|---|---|---|
| `sql` | mssql/server:2022-latest | 14333:1433 | — |
| `userservice` | Local build | (internal 8080) | sql |
| `productservice` | Local build | (internal 8080) | sql |
| `cartservice` | Local build | (internal 8080) | sql, productservice |
| `orderservice` | Local build | (internal 8080) | sql, productservice |
| `apigateway` | Local build | **5000:8080** | all services |
| `shopez-web` | Local build | **4200:80** | apigateway |

---

## Unit Testing

```bash
# Run all tests
dotnet test

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test CartService.Tests/
```

**Test Projects:**
- `CartService.Tests` — Cart business logic tests
- `OrderService.Tests` — Order creation and validation tests
- `ApiGateway.Tests` — Gateway smoke tests

---

## Key Design Decisions

- **Shared Database** — Single ECommerceDb for simplicity; can be separated per service in future
- **Ocelot Gateway** — Clean single entry point; JWT validated before forwarding to services
- **Dapper + EF Core** — Dapper for read-heavy product queries; EF Core for schema management and writes
- **Internal HTTP for inter-service** — CartService and OrderService call ProductService directly (not through gateway) for internal data fetching
- **Soft Delete** — Products are marked `IsActive = false` instead of hard-deleted (preserves order history integrity)
- **Denormalized Order Items** — Product name, price, and image are copied to OrderItem at creation time so order history is immune to future product changes

---

## Project Owner

**Maniar Roshan**
ShopEZ E-Commerce Application — May 2026
