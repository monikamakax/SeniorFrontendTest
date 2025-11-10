using System.Text.Json;
using CodeTest.Models;

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

            var props = typeof(Product).GetProperties()
                    .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);
            var filteredProducts = allProducts.Where(product =>
            {
                foreach (var kvp in filters)
                {
                    if (string.IsNullOrWhiteSpace(kvp.Value))
                        continue;

                    if (!props.TryGetValue(kvp.Key, out var prop))
                        return false;

                    var val = prop.GetValue(product)?.ToString() ?? "";

                    if (!val.Contains(kvp.Value, StringComparison.OrdinalIgnoreCase))
                        return false;
                }
                return true;
            }).ToList();

            return filteredProducts;
        }
    }
}