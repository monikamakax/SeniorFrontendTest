using System.Text.Json;
using CodeTest.Models;

namespace CodeTest.Services
{
    public interface IFilterService
    {
        Task<Dictionary<string, List<string?>>> GetAllAsync();
    }

    public class FilterService : IFilterService
    {
        private readonly IWebHostEnvironment _env;
        private readonly Lazy<Task<Dictionary<string, List<string?>>>> _cache;

        public FilterService(IWebHostEnvironment env)
        {
            _env = env;
            _cache = new(() => LoadAsync());
        }

        private async Task<Dictionary<string, List<string?>>> LoadAsync()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "products.json");
            await using var s = File.OpenRead(path);
            var items = await JsonSerializer.DeserializeAsync<List<Product>>(s,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var flavours = items?.Select(x => x.Flavour)?.Where(x => x != null)?.Distinct().ToList() ?? new List<string?>();
            var types = items?.Select(x => x.Type)?.Where(x => x != null)?.Distinct().ToList() ?? new List<string?>();
            return new Dictionary<string, List<string?>>{
                { "Flavours", flavours },
                { "Types", types } 
            };
        }

        public Task<Dictionary<string, List<string?>>> GetAllAsync() => _cache.Value;
    }
}