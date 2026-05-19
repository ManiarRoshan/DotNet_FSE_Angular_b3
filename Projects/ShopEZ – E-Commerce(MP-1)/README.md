# 🛒 ShopEZ — E-Commerce Frontend Application

> A responsive frontend e-commerce web application built with HTML5, CSS3, JavaScript, Bootstrap 5, and jQuery.

![HTML5](https://img.shields.io/badge/HTML5-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)
![Bootstrap](https://img.shields.io/badge/Bootstrap_5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)
![jQuery](https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white)

---

## 📌 About The Project

**ShopEZ** is a fully functional frontend e-commerce prototype that allows users to browse products, view product details, manage a shopping cart, and simulate a checkout process.

- ✅ No backend required — runs entirely on the client side
- ✅ Product data embedded directly in JavaScript
- ✅ Cart data stored using browser **LocalStorage**
- ✅ Fully responsive using **Bootstrap 5**
- ✅ Clean and beginner-friendly code structure

---

## 🖥️ Pages

| Page               | File                   | Description                                     |
| ------------------ | ---------------------- | ----------------------------------------------- |
| 🏠 Home            | `index.html`           | Hero section, category cards, featured products |
| 🛍️ Products        | `products.html`        | Full product catalog in a card grid             |
| 📋 Product Details | `product-details.html` | Image, description, price, add to cart          |
| 🛒 Cart            | `cart.html`            | Cart items, remove button, order summary        |
| 💳 Checkout        | `checkout.html`        | Delivery form, order summary, success screen    |

---

## 🚀 Getting Started

### Prerequisites

- [VS Code](https://code.visualstudio.com/) — free code editor
- [Live Server Extension](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer) — to run the project locally

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/your-username/ShopEZ.git
   ```

2. **Open the project folder in VS Code**

   ```
   File > Open Folder > Select ShopEZ folder
   ```

3. **Run with Live Server**
   - Right-click `index.html` in the Explorer panel
   - Click **"Open with Live Server"**
   - Browser opens at `http://127.0.0.1:5500/ShopEZ/`

> ⚠️ **Do NOT open index.html by double-clicking.** Always use Live Server — direct file open blocks JavaScript from working correctly.

---

## 📁 Folder Structure

```
ShopEZ/
│
├── index.html                 # Home page
├── products.html              # Product listing page
├── product-details.html       # Product detail page
├── cart.html                  # Shopping cart page
├── checkout.html              # Checkout page
│
├── css/
│   └── styles.css             # All custom styles
│
├── js/
│   ├── common.js              # Shared utilities (cart count, toast)
│   ├── cart.js                # Cart logic (add, remove, total)
│   ├── products.js            # Product data + display functions
│   └── checkout.js            # Checkout form + order logic
│
├── data/
│   └── products.json          # Product data (reference)
│
├── images/                    # Local images (optional)
└── lib/                       # Local Bootstrap/jQuery (optional)
```

---

## ✨ Features

- 🏠 **Home Page** — Hero banner, category highlights, featured products
- 🛍️ **Product Listing** — All 8 products in a responsive card grid
- 🔍 **Product Details** — Full product info with add to cart button
- 🛒 **Shopping Cart** — Add/remove items, live total calculation
- 💾 **Cart Persistence** — Cart saved in LocalStorage (survives page refresh)
- 💳 **Checkout Simulation** — Form with validation, order summary, success screen
- 📱 **Fully Responsive** — Works on desktop, tablet, and mobile
- 🔔 **Toast Notifications** — Popup feedback when items are added/removed

---

## 🧠 JavaScript Modules

### `common.js`

Shared utilities loaded on every page.

- `updateCartCount()` — Updates navbar cart badge
- `showToast(message)` — Shows popup notification
- `formatPrice(price)` — Formats number as ₹ Indian Rupee

### `cart.js`

All cart operations using LocalStorage.

- `addToCart(product)` — Adds product to cart
- `removeFromCart(index)` — Removes item by index
- `calculateTotal(cart)` — Returns sum of all item prices
- `getCart()` — Returns cart array from LocalStorage
- `clearCart()` — Clears cart after order is placed

### `products.js`

Product data and display functions.

- `allProducts` — Array of 8 product objects
- `loadProducts(callback)` — Passes product data to callback
- `buildProductCard(product)` — Returns HTML string for a product card
- `displayProducts(list, containerId)` — Renders cards into the page

### `checkout.js`

Handles the checkout page.

- Loads cart items into order summary
- Validates form fields before submission
- Clears cart and shows success screen on order placement

---

## 🎨 Design Details

| Property        | Value                  |
| --------------- | ---------------------- |
| Primary Color   | `#FF6B35` (Orange)     |
| Secondary Color | `#1A1A2E` (Dark Navy)  |
| Font            | Poppins (Google Fonts) |
| Icons           | Font Awesome 6         |
| UI Framework    | Bootstrap 5            |

---

## 📦 Tech Stack

| Technology   | Version     | Purpose              |
| ------------ | ----------- | -------------------- |
| HTML5        | Latest      | Page structure       |
| CSS3         | Latest      | Styling & animations |
| JavaScript   | ES6         | Application logic    |
| Bootstrap    | 5.3.0       | Responsive layout    |
| jQuery       | 3.7.0       | DOM & events         |
| Font Awesome | 6.4.0       | Icons                |
| LocalStorage | Browser API | Cart persistence     |

> All libraries are loaded via **CDN** — no npm install needed.

---

## 🔗 CDN Links Used

```html
<!-- Bootstrap 5 CSS -->
<link
  rel="stylesheet"
  href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
/>

<!-- Font Awesome Icons -->
<link
  rel="stylesheet"
  href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"
/>

<!-- jQuery -->
<script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>

<!-- Bootstrap 5 JS -->
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
```

---

## 🗂️ Product Data Structure

Each product in `js/products.js` follows this format:

```json
{
  "id": 1,
  "name": "Laptop",
  "description": "High performance laptop with Intel i5 processor...",
  "price": 60000,
  "category": "Electronics",
  "image": "https://images.unsplash.com/..."
}
```

To add a new product, open `js/products.js` and add a new object to the `allProducts` array with a unique `id`.

---

## 🧪 Testing Checklist

| Test                     | Expected Result                                   |
| ------------------------ | ------------------------------------------------- |
| Home page loads          | Hero, categories, and 4 featured products visible |
| Products page loads      | All 8 product cards displayed                     |
| View product details     | Details page shows name, price, description       |
| Add to cart              | Toast appears, cart badge count increases         |
| Cart page shows items    | Products listed with price and remove button      |
| Remove from cart         | Item removed, total updates                       |
| Cart total correct       | Subtotal + ₹99 shipping = final total             |
| Cart persists on refresh | Items still in cart after page reload             |
| Checkout form validation | Error if any field is empty                       |
| Place order              | Success screen shown, cart cleared                |
| Mobile responsive        | Layout adjusts, navbar collapses                  |

---

## ⚠️ Common Issues

**Products not loading?**
→ You opened the file by double-clicking. Use Live Server instead.

**No styles applied?**
→ Check that your HTML files are in the root `ShopEZ/` folder, not inside a subfolder.

**Cart count not updating?**
→ Make sure script tags load in this order: `jquery` → `bootstrap` → `common.js` → `cart.js` → `products.js`

**Checkout goes back to cart immediately?**
→ This is correct behavior. Checkout redirects to cart if it's empty.

---

## 📈 Future Improvements

- [ ] Product search bar
- [ ] Filter products by category
- [ ] Quantity update in cart
- [ ] Admin panel to add/edit products
- [ ] Product ratings and reviews
- [ ] Backend API integration using `fetch()`
- [ ] User login and registration

---

## 📄 License

This project is open source and available for educational use.

---

## 🙌 Acknowledgements

- [Bootstrap](https://getbootstrap.com/) — UI framework
- [jQuery](https://jquery.com/) — DOM manipulation
- [Font Awesome](https://fontawesome.com/) — Icons
- [Google Fonts](https://fonts.google.com/) — Poppins font
- [Unsplash](https://unsplash.com/) — Product images

---

<p align="center">Made with ❤️ | ShopEZ &copy; 2026</p>
