using System.Text.Json;
using CodeTest.Models;
using System.Reflection;

namespace CodeTest.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetFilteredAsync(Dictionary<string, string> filters);
    }

    public class ProductService : IProductService
    {
        private readonly IWebHostEnvironment _env;
        private readonly Lazy<Task<List<Product>>> _cache;

        public ProductService(IWebHostEnvironment env)
        {
            _env = env;
            _cache = new(() => LoadAsync());
        }

        private async Task<List<Product>> LoadAsync()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "products.json");
            await using var s = File.OpenRead(path);
            var items = await JsonSerializer.DeserializeAsync<List<Product>>(s,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return items ?? [];
        }

        public Task<List<Product>> GetAllAsync() => _cache.Value;

        public async Task<List<Product>> GetFilteredAsync(Dictionary<string, string> filters)
        {
            var allProducts = await GetAllAsync();

            if (filters == null || !filters.Any())
                return allProducts;
            var filteredProducts = allProducts;
            // Todo: Implement filtering logic based on the filters dictionary

            return filteredProducts;
        }
    }
}