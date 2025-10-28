using CodeTest.Models;
using CodeTest.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeTest.Pages;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public List<Product> Products { get; private set; } = [];

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public void OnGet()
    {
        // get all products from the product service
        Products = _productService.GetAllAsync().Result.ToList();
    }

}
