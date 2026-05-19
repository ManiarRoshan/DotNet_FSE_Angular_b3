// =============================================
// cart.js — Cart logic (add, remove, total)
// =============================================

// Add a product to cart (stored in LocalStorage)
function addToCart(product) {
  var cart = JSON.parse(localStorage.getItem("cart")) || [];
  cart.push(product);
  localStorage.setItem("cart", JSON.stringify(cart));
  updateCartCount();
  showToast(product.name + " added to cart!");
}

// Remove a product from cart by its index
function removeFromCart(index) {
  var cart = JSON.parse(localStorage.getItem("cart")) || [];
  cart.splice(index, 1);
  localStorage.setItem("cart", JSON.stringify(cart));
  updateCartCount();
}

// Calculate total price of all items in cart
function calculateTotal(cart) {
  var total = 0;
  for (var i = 0; i < cart.length; i++) {
    total = total + cart[i].price;
  }
  return total;
}

// Get cart array from LocalStorage
function getCart() {
  return JSON.parse(localStorage.getItem("cart")) || [];
}

// Clear all cart items
function clearCart() {
  localStorage.removeItem("cart");
  updateCartCount();
}
