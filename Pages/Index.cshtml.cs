using CodeTest.Models;
using CodeTest.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeTest.Pages;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly IFilterService _filterService;

    public List<Product> Products { get; private set; } = [];
    public Dictionary<string, List<string?>> Filters { get; private set; } = new Dictionary<string, List<string?>>();

    public IndexModel(IProductService productService, IFilterService filterService)
    {
        _productService = productService;
        _filterService = filterService;
    }

    public void OnGet()
    {
        // get all products from the product service
        Products = _productService.GetAllAsync().Result.ToList();
        Filters = _filterService.GetAllAsync().Result;
    }

}
