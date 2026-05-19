// =============================================
// products.js — Load and display products
// =============================================

// Load all products from JSON file
function loadProducts(callback) {
  $.getJSON("data/products.json", function (products) {
    callback(products);
  });
}

// Build HTML for a single product card
function buildProductCard(product) {
  return (
    '<div class="col-sm-6 col-lg-4 col-xl-3 mb-4">' +
      '<div class="product-card">' +
        '<img src="' + product.image + '" alt="' + product.name + '">' +
        '<div class="card-body">' +
          '<span class="category-badge"><i class="fa fa-tag me-1"></i>' + product.category + '</span>' +
          '<div class="card-title">' + product.name + '</div>' +
          '<p class="card-text">' + product.description + '</p>' +
          '<div class="d-flex justify-content-between align-items-center mb-2">' +
            '<span class="product-price">' + product.price.toLocaleString("en-IN") + '</span>' +
            '<a href="product-details.html?id=' + product.id + '" class="btn-view"><i class="fa fa-eye me-1"></i>View</a>' +
          '</div>' +
          '<button class="btn-cart" onclick=\'addToCart(' + JSON.stringify(product) + ')\'>' +
            '<i class="fa fa-cart-plus me-1"></i>Add to Cart' +
          '</button>' +
        '</div>' +
      '</div>' +
    '</div>'
  );
}

// Show all product cards in a container
function displayProducts(products, containerId) {
  var html = "";
  for (var i = 0; i < products.length; i++) {
    html = html + buildProductCard(products[i]);
  }
  $(containerId).html(html);
}

