
// =============================================
// checkout.js — Checkout page logic
// =============================================

$(document).ready(function () {

  // Load order summary from cart
  var cart = getCart();

  if (cart.length === 0) {
    // If cart is empty, redirect to cart page
    window.location.href = "cart.html";
    return;
  }

  // Display order summary items
  var html = "";
  for (var i = 0; i < cart.length; i++) {
    html = html +
      '<div class="summary-row">' +
        '<span>' + cart[i].name + '</span>' +
        '<span>' + formatPrice(cart[i].price) + '</span>' +
      '</div>';
  }
  $("#orderItems").html(html);

  // Show subtotal
  var subtotal = calculateTotal(cart);
  var shipping = 99;
  var total = subtotal + shipping;

  $("#orderSubtotal").text(formatPrice(subtotal));
  $("#orderShipping").text(formatPrice(shipping));
  $("#orderTotal").text(formatPrice(total));

  // Handle checkout form submit
  $("#checkoutForm").submit(function (e) {
    e.preventDefault();

    // Get form values
    var name = $("#custName").val().trim();
    var email = $("#custEmail").val().trim();
    var address = $("#custAddress").val().trim();

    // Simple check — all fields must be filled
    if (name === "" || email === "" || address === "") {
      showToast("Please fill in all the fields.");
      return;
    }

    // Simulate placing order — clear cart and show success
    clearCart();
    $("#checkoutSection").hide();
    $("#successSection").show();
  });

});
