import { map, computed } from "nanostores";

// Define the cart item interface
interface CartItem {
  id: string;
  name: string;
  price: number;
  quantity: number;
}

export const cartStore = map<Record<string, CartItem>>({});

// Computed store for the total number of items in the cart
export const cartItemCount = computed(cartStore, (cart) => {
  return Object.values(cart).reduce((total, item) => total + item.quantity, 0);
});

export function addToCart(id: string, name: string, price: number) {
  const currentCart = cartStore.get();

  if (currentCart[id]) {
    // If item exists, increment quantity
    cartStore.setKey(id, {
      ...currentCart[id],
      quantity: currentCart[id].quantity + 1,
    });
  } else {
    // If item doesn't exist, add it with quantity 1
    cartStore.setKey(id, {
      id,
      name,
      price,
      quantity: 1,
    });
  }
}

export function initCart() {
  cartItemCount.listen((count) => {
    updateCartBadge(count);
  });

  // Set up event listeners for add to cart buttons
  const addToCartButtons = document.querySelectorAll(".add-to-cart-btn");
  addToCartButtons.forEach((button, index) => {
    button.addEventListener("click", (e) => {
      const productCard = (e.target as HTMLElement).closest(".product-card");
      if (productCard) {
        const name =
          productCard.querySelector(".product-name")?.textContent || "Product";
        const priceText =
          productCard.querySelector(".product-price")?.textContent || "$0";
        const price = parseFloat(priceText.replace("$", ""));
        const id = `product-${index}`; // Using index as ID, you could use actual product ID if available

        addToCart(id, name, price);
      }
    });
  });
}

function updateCartBadge(count: number) {
  const cartIcon = document.querySelector(".header-cart");

  if (!cartIcon) return;

  // Remove existing badge if any
  const existingBadge = cartIcon.querySelector(".cart-badge");
  if (existingBadge) {
    existingBadge.remove();
  }

  // Add new badge if count > 0
  if (count > 0) {
    const badge = document.createElement("span");
    badge.className = "cart-badge";
    badge.textContent = count.toString();
    cartIcon.appendChild(badge);
  }
}
