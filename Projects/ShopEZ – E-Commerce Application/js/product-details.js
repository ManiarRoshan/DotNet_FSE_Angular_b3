$(document).ready(function () {

      // Get product id from URL query string
      var urlParams = new URLSearchParams(window.location.search);
      var productId = parseInt(urlParams.get("id"));

      if (!productId) {
        $("#loadingState").hide();
        $("#notFound").show();
        return;
      }

      // Load products JSON and find matching product
      loadProducts(function (products) {
        var product = null;

        for (var i = 0; i < products.length; i++) {
          if (products[i].id === productId) {
            product = products[i];
            break;
          }
        }

        $("#loadingState").hide();

        if (!product) {
          $("#notFound").show();
          return;
        }

        // Fill in product details
        $("#detailImg").attr("src", product.image).attr("alt", product.name);
        $("#detailCategory").html('<i class="fa fa-tag me-1"></i>' + product.category);
        $("#detailName").text(product.name);
        $("#detailDescription").text(product.description);
        $("#detailPrice").text("₹" + product.price.toLocaleString("en-IN"));

        // Add to cart button click
        $("#addToCartBtn").click(function () {
          addToCart(product);
        });

        $("#productDetails").show();
      });

    });