using System.Text.Json;
using CodeTest.Models;

namespace CodeTest.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
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
    }
}