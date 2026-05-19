// =============================================
// common.js — Shared utilities for all pages
// =============================================

// Update cart count badge in navbar
function updateCartCount() {
  var cart = JSON.parse(localStorage.getItem("cart")) || [];
  $("#cartCount").text(cart.length);
}

// Show a small toast notification
function showToast(message) {
  $("#shopezToast").text(message).addClass("show");
  setTimeout(function () {
    $("#shopezToast").removeClass("show");
  }, 2500);
}

// Format price with Indian Rupee
function formatPrice(price) {
  return "₹" + price.toLocaleString("en-IN");
}

// Run on every page load
$(document).ready(function () {
  updateCartCount();
});

