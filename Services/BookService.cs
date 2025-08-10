
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
        private readonly IHistorialRepository _historialRepository; // Para persistir en BD

        public BookService(
            IHttpClientFactory httpClientFactory,
            ILogger<BookService> logger,
            IMemoryCache cache,
            IHistorialRepository historialRepository)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _cache = cache;
            _historialRepository = historialRepository;
        }

        public async Task<List<BookResultDto>> SearchByAuthorAsync(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return new List<BookResultDto>();

            // Evitar repetición de búsquedas en menos de 1 minuto
            if (_cache.TryGetValue(author.ToLower(), out List<BookResultDto> cachedResults))
            {
                _logger.LogInformation("Resultado en caché para el autor {Author}", author);
                return cachedResults;
            }

            var client = _httpClientFactory.CreateClient("OpenLibrary");
            var url = $"search.json?author={Uri.EscapeDataString(author)}";

            try
            {
                using var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("OpenLibrary respondió {Status} para el autor {Author}", response.StatusCode, author);
                    return new List<BookResultDto>();
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = await JsonSerializer.DeserializeAsync<OpenLibrarySearchResponse>(stream, options)
                             ?? new OpenLibrarySearchResponse();

                var fechaConsulta = DateTime.Now;

                var mapped = result.Docs
                    .Where(d => !string.IsNullOrWhiteSpace(d.Title)) // Filtrar vacíos
                    .Select(d => new BookResultDto
                    {
                        Title = d.Title ?? "Sin título",
                        Year = d.FirstPublishYear ?? d.PublishYear?.OrderBy(y => y).FirstOrDefault(),
                        Publisher = d.Publisher?.FirstOrDefault(),
                        Autor = d.AuthorName?.FirstOrDefault() ?? author,
                        FechaConsulta = fechaConsulta
                    })
                    .ToList();

                // Guardar resultados en BD
                foreach (var book in mapped)
                {
                    await _historialRepository.GuardarBusquedaAsync(book);
                }

                // Guardar en caché por 1 minuto
                _cache.Set(author.ToLower(), mapped, TimeSpan.FromMinutes(1));

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