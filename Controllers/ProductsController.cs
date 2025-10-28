using Microsoft.AspNetCore.Mvc;
using CodeTest.Services;
using CodeTest.Models;

namespace CodeTest.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllAsync();
            return Json(products);
        }

        // api/products/filter
        [HttpPost("filter")]
        public async Task<IActionResult> FilterProducts([FromBody] Dictionary<string, string> filters)
        {
            var filteredProducts = await _productService.GetFilteredAsync(filters);

            // Return the partial view as HTML
            return PartialView("~/Pages/Shared/Partials/_ProductList.cshtml", filteredProducts);
        }
    }
}