export const setUpFilterListener = () => {
  const filterButton = document.querySelector<HTMLElement>(
    '[data-event="filter-products"]'
  );

  filterButton?.addEventListener("click", async (e) => {
    e.preventDefault();

    // Build filter dictionary
    const filters: Record<string, string> = {};
    const selects = document.querySelectorAll<HTMLSelectElement>("select.form-select");

    selects.forEach((select) => {
      const key = select.getAttribute("key");
      const val = select.value ?? "";
      if (key) filters[key] = val;
    });

    try {
      const response = await fetch("/api/products/filter", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(filters),
      });

      if (!response.ok) {
        throw new Error("Failed to fetch filtered products");
      }

      // The API returns partial HTML (not JSON)
      const html = await response.text();
      console.log(html);
      // Replace product list content
      const productListContainer = document.querySelector<HTMLElement>(
        ".product-list-wrapper"
      );
      if (productListContainer) {
        productListContainer.innerHTML = html;
      }
    } catch (err) {
      console.error("Error filtering products:", err);
    }
});

};



