using BookRadar.Models;
using BookRadar.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BookRadar.Services
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BookService> _logger;
        private readonly IMemoryCache _cache;

        public BookService(IHttpClientFactory httpClientFactory, ILogger<BookService> logger, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<BookResultDto>> SearchByAuthorAsync(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return new List<BookResultDto>();

            var client = _httpClientFactory.CreateClient("OpenLibrary");
            var url = $"search.json?author={Uri.EscapeDataString(author)}";

            try
            {
                using var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("OpenLibrary responded {Status} for author {Author}", response.StatusCode, author);
                    return new List<BookResultDto>();
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = await JsonSerializer.DeserializeAsync<OpenLibrarySearchResponse>(stream, options)
                             ?? new OpenLibrarySearchResponse();

                var mapped = result.Docs.Select(d => new BookResultDto
                {
                    Title = d.Title ?? "Sin título",
                    Year = d.FirstPublishYear ?? d.PublishYear?.OrderBy(y => y).FirstOrDefault(),
                    Publisher = d.Publisher?.FirstOrDefault(),
                    Autor = d.Autor ?? "Sin autor",
                    FechaConsulta = new DateTime()
                }).ToList();

                return mapped;
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Búsqueda cancelada para {Author}", author);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consumiendo OpenLibrary para {Author}", author);
                return new List<BookResultDto>();
            }
        }
    }
}